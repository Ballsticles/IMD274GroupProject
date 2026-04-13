using System.Collections;
using UnityEngine;

public class BaseDamagable : MonoBehaviour, IDamagable
{
    public int maxHealth;
    public int health;
    public bool invincible = false;
    public Material blinkMaterial;
    public GameObject hitEffectPrefab;
    private ParticleSystem hitEffect;
    public virtual void Awake()
    {
        health = maxHealth;
        if(hitEffect != null)
        {
            GameObject hiteffectObj = Instantiate(hitEffectPrefab);
            hitEffect = hiteffectObj.GetComponent<ParticleSystem>();
            
        }

    }
    IEnumerator DamageFlashEffect()
    {
        blinkMaterial.SetFloat("blinkFactor", .5f);

        yield return new WaitForSeconds(.5f);

        blinkMaterial.SetFloat("blinkFactor", 0f);

    }
    public virtual void TakeDamage(int damage)
    {
        if (!invincible)
        {
            health -= damage;
            if(blinkMaterial != null)
            {
                StartCoroutine(DamageFlashEffect());
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
