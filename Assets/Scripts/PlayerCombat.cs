using System.Collections.Generic;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{
    [SerializeField] int attackRange;
    public List<AttackSO> combo;
    List<Timer> timers;
    [SerializeField] float attackCD = 0.5f;
    CountdownTimer attackCDTimer;
    [SerializeField] float comboCD = 0.2f;
    CountdownTimer comboCDTimer;
    int comboCounter;
    [SerializeField] InputManager inputManager;
    [SerializeField] float attackCDProgress;
    [SerializeField] float comboCDProgress;
    Animator anim;
    [SerializeField]private GameObject currWeaponCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
     
        inputManager.Attack += OnAttack;
       
    }

    void OnDisable()
    {
  
        inputManager.Attack -= OnAttack;
        
    }
    void Awake()
    {
        attackCDTimer = new CountdownTimer(attackCD);
        comboCDTimer = new CountdownTimer(comboCD);

        timers = new List<Timer>(2) { attackCDTimer, comboCDTimer};
        
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TickTimers();
        ExitAttack();

        attackCDProgress = attackCDTimer.Progress;
        comboCDProgress = comboCDTimer.Progress;

    }
    public void OnAttack(bool performed)
    {
        if(performed)
        {
            Attack();
        }
        if (!performed)
        {
            //noop
        }
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
               
                AttackSO attack = combo[comboCounter];
                anim.runtimeAnimatorController = attack.animatorOV;

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
            }

            if (comboCounter >= combo.Count)
            {
                comboCounter = 0;
                comboCDTimer.Start();
            }

        }

    }

    void ExitAttack()
    {
        if (!attackCDTimer.IsRunning)
        {
            Destroy(currWeaponCollider);
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
    }
    void TickTimers()
    {
        foreach (var timer in timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }
}
