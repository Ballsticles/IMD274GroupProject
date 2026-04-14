using UnityEngine;

public class LedgeState : BaseState
{
    public LedgeState(PlayerMotor player, Animator animator, GroundChecker groundChecker, PlayerCombat combat) : base(player, animator, groundChecker, combat)
    {
    }
    public override void OnEnter()
    {
        animator.CrossFade(LedgeHash, crossFadeDuration);
        combat.canAttack = false;
    }
    public override void FixedUpdate()
    {
        player.HandleJump();
        
        
    }
    public override void OnExit()
    {
        base.OnExit();
        combat.canAttack = true;
    }
}