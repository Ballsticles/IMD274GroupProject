
using UnityEngine;

public interface IDamagable 
{
    void Update();
    void TakeDamage(int damage);
    void HealHealth(int health);
    void UpdateHealth();
    void Die();
}
