using UnityEngine;

public class DieState : BaseCombatState
{
    public DieState(PlayerCombat combat, Animator animator, Animator combatHUD, PlayerHealth playerHealth) : base(combat, animator, combatHUD, playerHealth) { }

    public override void OnEnter()
    {
        combat.DisableActions();
        animator.CrossFade(DieHash, crossFadeDuration);
    }

}