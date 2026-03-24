using UnityEngine;

public class FallState : BaseState
{
    

    public FallState(PlayerMotor player, Animator animator, GroundChecker groundChecker) : base(player, animator, groundChecker)
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