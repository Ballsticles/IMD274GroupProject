using UnityEngine;

public class HurtState : BaseCombatState
{
    public HurtState(PlayerCombat combat, Animator animator, Animator combatHUD, PlayerHealth playerHealth) : base(combat, animator, combatHUD, playerHealth) {}

    public override void OnEnter()
    {
        combatHUD.SetBool("ShowUI", true);
        animator.CrossFade(HurtHash, crossFadeDuration);
        playerHealth.invincible = true;
    }
    public override void OnExit()
    {
        playerHealth.invincible = false;
    }
}
