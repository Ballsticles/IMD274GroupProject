using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    public BoxCollider hurtColl;

    public virtual void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        //Debug.Log("hit" + damagable);
        if (damagable != null)
        {
            damagable.TakeDamage(damage);
        }
    }

}
