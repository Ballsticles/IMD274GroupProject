using UnityEngine;

public class OutCombatState : BaseCombatState
{
    public OutCombatState(PlayerCombat combat, Animator animator, Animator combatHUD, PlayerHealth playerHealth) : base(combat, animator, combatHUD, playerHealth)
    {
    }
    public override void OnEnter()
    {
        combatHUD.SetBool("ShowUI", false);
    }


}