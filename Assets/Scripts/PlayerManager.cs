using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    PlayerMotor playerMotor;

    private void Awake()
    {
        
        playerMotor = GetComponent<PlayerMotor>();

    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
