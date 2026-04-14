
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    public BoxCollider hurtColl;
    private void Awake()
    {
        
    }
    private void OnDrawGizmos()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        //Debug.Log("hit" + damagable);
        if (damagable != null)
        {
            damagable.TakeDamage(damage);
        }
    }

}
