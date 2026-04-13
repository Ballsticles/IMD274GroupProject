using UnityEngine;

public class CombatReadyState : BaseCombatState
{
    public CombatReadyState(PlayerInputActions input, Animator animator, Animator combatHUD, PlayerHealth playerHealth) : base(input, animator, combatHUD, playerHealth) { }

    public override void OnEnter()
    {
        EnableAllInputs();
        EnableMovementInputs();
        combatHUD.SetBool("ShowUI", true);

        animator.CrossFade(LocomotionHash, crossFadeDuration);
    }
}
