using UnityEngine;

public class LedgeState : BaseState
{
    public LedgeState(PlayerMotor player, Animator animator, GroundChecker groundChecker) : base(player, animator, groundChecker)
    {
    }
    public override void OnEnter()
    {
        animator.CrossFade(LedgeHash, crossFadeDuration);
    }
    public override void FixedUpdate()
    {
        player.HandleJump();
        
    }

}