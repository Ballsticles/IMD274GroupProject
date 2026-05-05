using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : BaseDamagable
{
    public Slider healthBar;
    public bool healthBarEnabled = false;
    Animator anim;
    public BoxCollider collisionHurtBox;

    public override void Awake()
    {
        base.Awake();
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
        anim = gameObject.GetComponentInChildren<Animator>();
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
            healthBar.gameObject.SetActive(true);
        }
        else
        {
            healthBar.gameObject.SetActive(false);
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
        if (collisionHurtBox != null)
        {
            collisionHurtBox.enabled = false;
        }
        anim.SetBool("dead", true);
    }

}