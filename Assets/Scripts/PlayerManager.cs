using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerMotor playerMotor;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerMotor = GetComponent<PlayerMotor>();

    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerMotor.HandleAllMovement();
    }
}
