using UnityEngine;

public class AttackState : BaseCombatState
{
    public AttackState(PlayerInputActions input, Animator animator, Animator combatHUD, PlayerHealth playerHealth) : base(input, animator, combatHUD, playerHealth) {}
    public override void OnEnter()
    {
        combatHUD.SetBool("ShowUI", true);
        DisableMovementInputs();
    }
}
