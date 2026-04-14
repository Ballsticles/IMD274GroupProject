using UnityEngine;

public class HealState : BaseCombatState
{
    public HealState(PlayerCombat combat, Animator animator, Animator combatHUD, PlayerHealth playerHealth) : base(combat, animator, combatHUD, playerHealth)
    {
    }
    public override void OnEnter()
    {
        combatHUD.SetBool("ShowUI", true);
    }


}