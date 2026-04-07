using UnityEngine;

public class BaseDamagable : MonoBehaviour, IDamagable
{
    public int maxHealth;
    public int health;
    public bool invincible = false;

    public virtual void Awake()
    {
        health = maxHealth;
    }
    public virtual void TakeDamage(int damage)
    {
        if (!invincible)
        {
            health -= damage;
            UpdateHealth();
        }
    }

    public virtual void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    public virtual void UpdateHealth()
    {
       
    }
    public virtual void Die()
    {

    }
}
