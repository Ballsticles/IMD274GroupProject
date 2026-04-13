using UnityEngine;

public class DiveState : BaseState
{
    public DiveState(PlayerMotor player, Animator animator, GroundChecker groundChecker, PlayerCombat combat) : base(player, animator, groundChecker, combat) { }

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