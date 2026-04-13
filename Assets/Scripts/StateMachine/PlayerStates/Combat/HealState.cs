using UnityEngine;

public class HealState : BaseCombatState
{
    public HealState(PlayerInputActions input, Animator animator, Animator combatHUD, PlayerHealth playerHealth) : base(input, animator, combatHUD, playerHealth)
    {
    }
    public override void OnEnter()
    {
        combatHUD.SetBool("ShowUI", true);
    }


}