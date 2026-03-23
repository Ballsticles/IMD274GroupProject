using UnityEngine;

public abstract class BaseState : IStates
{
    protected readonly PlayerMotor player;
    protected readonly Animator animator;

    protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    protected static readonly int JumpHash = Animator.StringToHash("Jump");
    protected static readonly int SwingHash = Animator.StringToHash("Swing");

    protected const float crossFadeDuration = 0.5f;

    protected BaseState(PlayerMotor player, Animator animator)
    {
        this.player = player;
        this.animator = animator;
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
        Debug.Log("BaseState.OnExit");
    }

}