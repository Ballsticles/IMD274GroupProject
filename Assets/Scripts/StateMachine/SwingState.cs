using UnityEngine;

public class SwingState : BaseState
{
    
    public SwingState(PlayerMotor player, Animator animator) : base(player, animator)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("SwingState.OnEnter");
        animator.CrossFade(SwingHash, crossFadeDuration);
    }

    public override void FixedUpdate()
    {
        player.HandleMovement();
    }

}
