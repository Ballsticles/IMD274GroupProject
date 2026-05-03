
using UnityEngine;

public class HealItem : MonoBehaviour
{
    public int incomingHealth;
    public BoxCollider healColl;

    private void Awake()
    {
        if (healColl == null)
        {
            healColl = GetComponent<BoxCollider>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        //Debug.Log("hit" + damagable);
        if (damagable != null)
        {
            damagable.HealHealth(incomingHealth);
        }
    }



}