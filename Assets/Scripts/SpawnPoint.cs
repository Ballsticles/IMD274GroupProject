using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static event Action<Transform> SetSpawnPoint;

    [SerializeField] public bool isInitialSpawnPoint = false;
    private bool usedSpawnPoint = false;


    private void Awake()
    {
        if (isInitialSpawnPoint)
        {
            SetSpawnPoint?.Invoke(transform);
            usedSpawnPoint = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !usedSpawnPoint)
        {
            SetSpawnPoint?.Invoke(transform);
            usedSpawnPoint = true;
            Collider col = GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false;
            }
        }
    }

}