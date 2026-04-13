using UnityEngine;

public class DieState : BaseCombatState
{
    public DieState(PlayerInputActions input, Animator animator, Animator combatHUD, PlayerHealth playerHealth) : base(input, animator, combatHUD, playerHealth) { }

    public override void OnEnter()
    {
        DisableAllInputs();
        animator.CrossFade(DieHash, crossFadeDuration);
    }

}