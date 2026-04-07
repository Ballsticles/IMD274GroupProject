using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : BaseDamagable
{
    public Slider healthBar;
    public bool healthBarEnabled = false;
    Animator anim;

    public override void Awake()
    {
        base.Awake();
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
        anim = gameObject.GetComponent<Animator>();
    }
    public override void UpdateHealth()
    {
        base.UpdateHealth();
        healthBar.value = health;

    }

    public override void Update() 
    { 
        base.Update(); 
        if (healthBarEnabled)
        {
            healthBar.enabled = true;
        }
        else
        {
            healthBar.enabled = false;
        }
        UpdateHealth();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        base.Die();
        anim.SetBool("dead", true);
    }

}