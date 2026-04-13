using UnityEngine;

public class LocomotionState : BaseState
{
    public LocomotionState(PlayerMotor player, Animator animator, GroundChecker groundChecker, PlayerCombat combat) : base(player, animator, groundChecker, combat) { }

    public override void OnEnter()
    {
        
        animator.CrossFade(LocomotionHash, crossFadeDuration);
        
    }

    public override void FixedUpdate()
    {
        player.HandleMovement();
        groundChecker.CheckForGround();
    }
}

