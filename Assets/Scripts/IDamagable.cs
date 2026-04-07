
using UnityEngine;

public interface IDamagable 
{
    void Update();
    void TakeDamage(int damage);
    void UpdateHealth();
    void Die();
}
