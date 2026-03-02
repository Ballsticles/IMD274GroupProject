using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    InputManager inputManager;
    
    CharacterController characterController;

    Vector3 moveDirection;
    
    Transform cameraObject;
    
    [SerializeField]
    float movementSpeed = 7;
    [SerializeField]
    float rotationSpeed = 15;
    [SerializeField]
    float gravityForce = 9.8f;
    [SerializeField]
    float jumpForce = 10;

    private bool jumpInput;
    public bool isGrounded; 


    public bool isJumping = false;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
        HandleJump();
    }


    private void HandleMovement()
    {
        //calculates the movement direction by taking the camera's looking position and the input floats from the inputmanager script
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        //makes it so the player cannot move up into the sky
        moveDirection.y = 0;
        //Adds gravitational force to the character
        moveDirection.y -= gravityForce * Time.deltaTime * 10 ;
        moveDirection = moveDirection * movementSpeed * Time.deltaTime;

        characterController.Move(moveDirection);
    }

    private void HandleJump()
    {
        jumpInput = inputManager.jumpInput;
        isGrounded = characterController.isGrounded;

        
        if (jumpInput && isGrounded)
        {
            moveDirection.y += jumpForce * Time.deltaTime;
            isJumping = true;
            characterController.Move(moveDirection);
        }
        else
        {
            isJumping = false;
        }



    }






    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;
        
        if(targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;

    }



}
