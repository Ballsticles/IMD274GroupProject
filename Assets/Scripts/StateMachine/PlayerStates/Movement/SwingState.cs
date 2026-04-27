using UnityEngine;

public class SwingState : BaseState
{
    
    public SwingState(PlayerMotor player, Animator animator, GroundChecker groundChecker, PlayerCombat combat) : base(player, animator, groundChecker, combat)
    {
    }

    public override void OnEnter()
    {
        player.PlaySwingSound();
        animator.CrossFade(SwingHash, crossFadeDuration);
        player.swinging = true;
        combat.canAttack = false;
    }

    public override void OnExit()
    {
        player.swinging = false;
        player.StartFallTimer();
        combat.canAttack = true;
        player.StopSwingSound();
    }
}
