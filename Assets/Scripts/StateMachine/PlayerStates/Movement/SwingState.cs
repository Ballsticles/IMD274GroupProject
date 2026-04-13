using UnityEngine;

public class SwingState : BaseState
{
    
    public SwingState(PlayerMotor player, Animator animator, GroundChecker groundChecker, PlayerCombat combat) : base(player, animator, groundChecker, combat)
    {
    }

    public override void OnEnter()
    {
        
        animator.CrossFade(SwingHash, crossFadeDuration);
        player.swinging = true;
    }

    public override void OnExit()
    {
        player.swinging = false;
        player.StartFallTimer();
    }
}
