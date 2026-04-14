using UnityEngine;

public class AttackState : BaseCombatState
{
    public AttackState(PlayerCombat combat, Animator animator, Animator combatHUD, PlayerHealth playerHealth) : base(combat, animator, combatHUD, playerHealth) {}
    public override void OnEnter()
    {
        combatHUD.SetBool("ShowUI", true);
        combat.StopMovement();
    }
}
