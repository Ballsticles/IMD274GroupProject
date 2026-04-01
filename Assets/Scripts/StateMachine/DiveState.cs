using UnityEngine;

public class DiveState : BaseState
{
    public DiveState(PlayerMotor player, Animator animator, GroundChecker groundChecker) : base(player, animator, groundChecker) { }

    public override void OnEnter()
    {
        animator.CrossFade(DiveHash, crossFadeDuration);
    }

    public override void FixedUpdate()
    {
        groundChecker.CheckForGround();
        player.HandleMovement();
        player.HandleJump();
    }


}