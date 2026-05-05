using System.Collections;
using UnityEngine;

public class BaseDamagable : MonoBehaviour, IDamagable
{
    public int maxHealth;
    public int health;
    public bool invincible = false;
    private bool isDead = false;
    private DamageFlash flash;

    public GameObject hitEffectPrefab;
    private ParticleSystem hitEffect;
    public virtual void Awake()
    {
        health = maxHealth;
        flash = GetComponent<DamageFlash>();
    }


    public virtual void TakeDamage(int damage)
    {
        if (!invincible)
        {
            health -= damage;
    
            if(flash != null)
            {
                flash.CallDamageFlash();
            }
            
            if(hitEffect != null)
            {
                if (hitEffect.gameObject.activeSelf)
                {
                    hitEffect.Play();
                }
                else
                {
                    hitEffect.gameObject.SetActive(true);
                    hitEffect.Play();
                }
                    
            }

            UpdateHealth();
            


        }
    }
    public virtual void HealHealth(int incHealth)
    {
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += incHealth;
        }
            
        UpdateHealth();

    }


    public virtual void Update()
    {
        if(health <= 0 && !isDead)
        {
            Die();
        }



    }

    public virtual void UpdateHealth()
    {
       
    }
    public virtual void Die()
    {
        invincible = true;
        isDead = true;
    }
}
