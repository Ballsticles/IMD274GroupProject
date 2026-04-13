using UnityEngine;

public class OutCombatState : BaseCombatState
{
    public OutCombatState(PlayerInputActions input, Animator animator, Animator combatHUD, PlayerHealth playerHealth) : base(input, animator, combatHUD, playerHealth)
    {
    }
    public override void OnEnter()
    {
        combatHUD.SetBool("ShowUI", false);
    }


}