using UnityEngine;

public class SwingState : BaseState
{
    
    public SwingState(PlayerMotor player, Animator animator, GroundChecker groundChecker) : base(player, animator, groundChecker)
    {
    }

    public override void OnEnter()
    {
        
        animator.CrossFade(SwingHash, crossFadeDuration);
        player.swinging = true;
    }

    public override void FixedUpdate()
    {
        
    }

}
