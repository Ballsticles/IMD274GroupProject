using System;
using UnityEngine.Events;


public class PlayerHealth : BaseDamagable
{
    public static event Action OnPlayerHealthChanged;
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHeal;



    public override void UpdateHealth() 
    {
        OnPlayerHealthChanged?.Invoke();
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        OnPlayerDamaged?.Invoke();
    }
    public override void HealHealth(int incHealth)
    {
        base.HealHealth(incHealth);
        OnPlayerHeal?.Invoke();
    }
    public override void Die()
    {
        base.Die();
        OnPlayerDeath?.Invoke();
    }
}