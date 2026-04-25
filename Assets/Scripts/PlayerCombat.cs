
using System.Collections.Generic;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{
    [Header("Attacks")]
    [SerializeField] int attackRange;
    public List<AttackSO> combo;
    int comboCounter;
    List<Timer> timers;
    [Header("Timers")]
    [SerializeField] float attackCD = 0.5f;
    CountdownTimer attackCDTimer;
    [SerializeField] float comboCD = 0.2f;
    CountdownTimer comboCDTimer;
    [SerializeField] float hurtTime = 0.4f;
    CountdownTimer hurtTimer;
    [SerializeField] float outOfCombatTime = 1f;
    CountdownTimer outOfCombatTimer;
    [SerializeField] float healTime = 0.2f;
    CountdownTimer healTimer;

    [Header("Timer Progress")]
    [SerializeField] float attackCDProgress;
    [SerializeField] float comboCDProgress;
    [SerializeField] float hurtTimerProgress;
    [SerializeField] float combatCountdownProgress;

    [Header("References")]
    [SerializeField] InputManager inputManager;
    [SerializeField] PlayerMotor playerMotor;
    [SerializeField] PlayerHealth playerHealth;
    PlayerInputActions inputActions;
    [SerializeField] Animator combatHUD;
    Animator anim;
    [SerializeField] private GameObject currWeaponCollider;
    [SerializeField] private AudioSource combatAudio;

    [Header("SFX")]
    [SerializeField] private AudioClip[] hurtSounds;
    [SerializeField] private AudioClip[] healSounds;
    //statemachine stuffs
    StateMachine combatStateMachine;
    public string currentState;
    public bool canAttack = true;
     bool attacking = false;
     bool dying = false;
     bool inCombat = false;
     
    void OnEnable()
    {
        inputManager.Attack += OnAttack;
        PlayerHealth.OnPlayerDamaged += OnHurt;
        PlayerHealth.OnPlayerHeal += OnHeal;
        PlayerHealth.OnPlayerDeath += OnDie;
        
    }

    void OnDisable()
    {
  
        inputManager.Attack -= OnAttack;
        PlayerHealth.OnPlayerDamaged -= OnHurt;
        PlayerHealth.OnPlayerHeal -= OnHeal;
        PlayerHealth.OnPlayerDeath -= OnDie;
    }
    void Awake()
    {
        attackCDTimer = new CountdownTimer(attackCD);
        comboCDTimer = new CountdownTimer(comboCD);
        hurtTimer = new CountdownTimer(hurtTime);
        outOfCombatTimer = new CountdownTimer(outOfCombatTime);
        healTimer = new CountdownTimer(healTime);
        timers = new List<Timer>(2) { attackCDTimer, comboCDTimer, hurtTimer, outOfCombatTimer, healTimer};
        
        inputActions = inputManager.inputActions;
        anim = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        playerMotor = GetComponent<PlayerMotor>();
        combatHUD = GameObject.FindGameObjectWithTag("CombatUI").GetComponent<Animator>();

        comboCDTimer.onTimerStop += () => outOfCombatTimer.Start();
        hurtTimer.onTimerStop += () => outOfCombatTimer.Start();
        healTimer.onTimerStop += () => outOfCombatTimer.Start();
        outOfCombatTimer.onTimerStart += () => inCombat = true;
        outOfCombatTimer.onTimerStop += () => inCombat = false;

        combatStateMachine = new StateMachine();

        //states stuffs
        var attackState = new AttackState(this, anim, combatHUD, playerHealth);
        var combatReadyState = new CombatReadyState(this, anim, combatHUD, playerHealth);
        var dieState = new DieState(this, anim, combatHUD, playerHealth);
        var HurtState = new HurtState(this, anim, combatHUD, playerHealth);
        var outCombatState = new OutCombatState(this, anim, combatHUD, playerHealth);
        var healState = new HealState(this, anim, combatHUD, playerHealth);

        Any(attackState, new FuncPredicate(() => attacking));
        At(attackState, combatReadyState, new FuncPredicate(()=> !attacking && inCombat));

        Any(healState, new FuncPredicate(() => healTimer.IsRunning));

        Any(HurtState, new FuncPredicate(()=> hurtTimer.IsRunning && !dying));
        At(HurtState, combatReadyState, new FuncPredicate(() => !hurtTimer.IsRunning && inCombat));

        Any(dieState, new FuncPredicate(() => dying));

        At(combatReadyState, outCombatState, new FuncPredicate(() => !inCombat));
        Any(combatReadyState, new FuncPredicate(() => inCombat && (!dying || !attacking || !hurtTimer.IsRunning || !healTimer.IsRunning)));

        At(outCombatState, combatReadyState, new FuncPredicate(() => inCombat));
        Any(outCombatState, new FuncPredicate(()=> !inCombat && (!dying || !attacking || !hurtTimer.IsRunning || !healTimer.IsRunning)));


        combatStateMachine.SetState(outCombatState);

    }
    void At(IStates from, IStates to, IPredicate condition) => combatStateMachine.AddTransition(from, to, condition);
    void Any(IStates to, IPredicate condition) => combatStateMachine.AddAnyTransition(to, condition);
    // Update is called once per frame
    void Update()
    {
        TickTimers();
        ExitAttack();
        combatStateMachine.Update();

        attackCDProgress = attackCDTimer.Progress;
        comboCDProgress = comboCDTimer.Progress;
        hurtTimerProgress = hurtTimer.Progress;
        combatCountdownProgress = outOfCombatTimer.Progress;
        currentState = combatStateMachine.current.State.ToString();
    }
    public void OnAttack(bool performed)
    {
        if(performed && canAttack)
        {
            Attack();
        }
    }

    public void OnHurt()
    {
        if(currentState != "HurtState")
        {
            hurtTimer.Start();
            combatAudio.PlayOneShot(hurtSounds[Random.Range(0, hurtSounds.Length)]);
        }
    }
    public void OnDie()
    {
        dying = true;
    }
    public void OnHeal()
    {
        healTimer.Start();
        combatAudio.PlayOneShot(healSounds[Random.Range(0, healSounds.Length)]);

    }
    void Attack()
    {
        
        //check if the combo is on cooldown and the attack is below the max combo.
        if (!comboCDTimer.IsRunning && comboCounter <= combo.Count)
        {

            CancelInvoke("EndCombo");

            //check if attack is on cooldown
            if (!attackCDTimer.IsRunning)
            {
                attacking = true;
                
                outOfCombatTimer.Start();
                AttackSO attack = combo[comboCounter];
                anim.runtimeAnimatorController = attack.animatorOV;
                combatAudio.PlayOneShot(attack.attackSound);
                if (currWeaponCollider != null)
                {
                    Destroy(currWeaponCollider);
                    currWeaponCollider = Instantiate(attack.hurtCollider.gameObject, this.gameObject.transform, false);
                }
                else
                {
                    currWeaponCollider = Instantiate(attack.hurtCollider.gameObject, this.gameObject.transform, false);
                }
                Weapon currWeapon = currWeaponCollider.GetComponent<Weapon>();
                
                currWeapon.damage = attack.damage;
                //Vector3 weapCollCenter = weaponColl.center;
                //weapCollCenter.z += attackRange;
                //weaponColl.center = weapCollCenter;

                
                anim.Play("Attack", 1, 0);
                comboCounter++;
                
                attackCDTimer.Start();         
                
                if (comboCounter >= combo.Count)
                {
                    comboCounter = 0;
                    comboCDTimer.Start();
                }

            }



        }

    }
    void ExitAttack()
    {
        if (!attackCDTimer.IsRunning)
        {
            Destroy(currWeaponCollider);
            attacking = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(1).normalizedTime > .9 && anim.GetCurrentAnimatorStateInfo(1).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
            
        }
        
    }
    void EndCombo()
    {
        comboCounter = 0;

        comboCDTimer.Start();

        outOfCombatTimer.Start();
    }
    void TickTimers()
    {
        foreach (var timer in timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }

    public void StopMovement()
    {
        playerMotor.StopMovement();
    }
    public void StartMovement()
    {
        playerMotor.ContinueMovement();
    }
    public void DisableActions()
    {
        inputManager.DisablePlayerActions();
    }
    public void EnableActions()
    {
        inputManager.EnablePlayerActions();
    }

}
