using UnityEngine;

public class CombatReadyState : BaseCombatState
{
    public CombatReadyState(PlayerCombat combat, Animator animator, Animator combatHUD, PlayerHealth playerHealth) : base(combat, animator, combatHUD, playerHealth) { }

    public override void OnEnter()
    {
        combat.EnableActions();
        combat.StartMovement();
        combatHUD.SetBool("ShowUI", true);

        animator.CrossFade(LocomotionHash, crossFadeDuration);
    }
}
