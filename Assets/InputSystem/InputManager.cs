
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    [SerializeField]
    Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public bool jumpInput = false;

    private void OnEnable()
    {
        
        //Checks if there is no playercontrols input system
        if (playerControls == null)
        {
            //creates a new playercontrols
            playerControls = new PlayerControls();

            //grabs the movement input from playercontrols and assigns the values to the movementInput vector2
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            
        }

        //enables the playercontrols
        playerControls.Enable();
    }

    private void OnDisable()
    {
        //disables the playercontrols
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleJumpInput();
        //HandleAttackInput
    }
    private void HandleMovementInput()
    {
        //assigns the vector2 values to seperate floats to be used by the playermotor script
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

    private void HandleJumpInput()
    {
        if (playerControls.PlayerMovement.Jump.IsPressed())
        {
            jumpInput = true;
        }
        else
        {
            jumpInput = false;
        }
    }
}
