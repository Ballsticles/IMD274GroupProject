using UnityEngine;

public class FallState : BaseState
{
    

    public FallState(PlayerMotor player, Animator animator, GroundChecker groundChecker, PlayerCombat combat) : base(player, animator, groundChecker, combat)
    {
    }
    public override void OnEnter()
    {
        
        
    }
    public override void FixedUpdate()
    {
        player.HandleJump();
        player.HandleMovement();
        groundChecker.CheckForGround();
        
    }


}