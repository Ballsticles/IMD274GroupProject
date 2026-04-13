using UnityEngine;

public abstract class BaseCombatState : IStates
{
    protected readonly PlayerInputActions input;
    protected readonly Animator animator;
    protected readonly Animator combatHUD;
    protected readonly PlayerHealth playerHealth;

    protected static readonly int DieHash = Animator.StringToHash("Die");
    protected static readonly int HurtHash = Animator.StringToHash("Hurt");
    protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");

    protected const float crossFadeDuration = 0.1f;
    protected BaseCombatState(PlayerInputActions input, Animator animator, Animator combatHUD, PlayerHealth playerHealth)
    {
        this.input = input;
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
    public virtual void DisableAllInputs()
    {
        input.Player.Disable();
    }
    public virtual void EnableAllInputs()
    {
        input.Player.Enable();
    }
    public virtual void DisableMovementInputs()
    {
        input.FindAction("Move").Disable();
        input.FindAction("Jump").Disable();
        input.FindAction("Grab").Disable();
        input.FindAction("Interact").Disable();

    }
    public virtual void EnableMovementInputs()
    {
        input.FindAction("Move").Enable();
        input.FindAction("Jump").Enable();
        input.FindAction("Grab").Enable();
        input.FindAction("Interact").Enable();
    }

}
