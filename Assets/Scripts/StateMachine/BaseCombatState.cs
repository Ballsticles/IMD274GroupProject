using UnityEngine;

public abstract class BaseCombatState : IStates
{
    protected readonly PlayerCombat combat;
    protected readonly Animator animator;
    protected readonly Animator combatHUD;
    protected readonly PlayerHealth playerHealth;

    protected static readonly int DieHash = Animator.StringToHash("Die");
    protected static readonly int HurtHash = Animator.StringToHash("Hurt");
    protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");

    protected const float crossFadeDuration = 0.1f;
    protected BaseCombatState(PlayerCombat combat, Animator animator, Animator combatHUD, PlayerHealth playerHealth)
    {
        this.combat = combat;
        this.animator = animator;
        this.combatHUD = combatHUD; 
        this.playerHealth = playerHealth;
    }
    
    
    public virtual void FixedUpdate()
    {
        
    }

    public virtual void OnEnter()
    {
        
    }

    public virtual void OnExit()
    {
        
    }

    public virtual void Update()
    {
        
    }


}
