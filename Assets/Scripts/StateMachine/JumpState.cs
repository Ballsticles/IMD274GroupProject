using UnityEngine;

public class JumpState : BaseState
{
    
    public JumpState(PlayerMotor player, Animator animator, GroundChecker groundChecker, PlayerCombat combat) : base(player, animator, groundChecker, combat) { }

    public override void OnEnter()
    {
        
        //animator.CrossFade(JumpHash, crossFadeDuration);
        
    }

    public override void FixedUpdate()
    {
        player.HandleJump();
        player.HandleMovement();
        
        groundChecker.CheckForGround();
    }

}
