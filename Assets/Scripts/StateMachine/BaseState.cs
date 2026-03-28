using UnityEngine;

public abstract class BaseState : IStates
{
    protected readonly PlayerMotor player;
    protected readonly Animator animator;
    protected readonly GroundChecker groundChecker;

    protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    protected static readonly int JumpHash = Animator.StringToHash("Jump");
    protected static readonly int SwingHash = Animator.StringToHash("Swing");
    protected static readonly int FallHash = Animator.StringToHash("Fall");
    protected static readonly int LedgeHash = Animator.StringToHash("Ledge");
    protected static readonly int DiveHash = Animator.StringToHash("Dive");


    protected const float crossFadeDuration = 0.2f;

    protected BaseState(PlayerMotor player, Animator animator, GroundChecker groundChecker)
    {
        this.player = player;
        this.animator = animator;
        this.groundChecker = groundChecker;
    }

    public virtual void OnEnter()
    {
        //noop
    }
    public virtual void Update()
    {
       //noop;
    }       
    public virtual void FixedUpdate()
    {
        //noop
    } 
    public virtual void OnExit()
    {
        //noop
        
    }

}