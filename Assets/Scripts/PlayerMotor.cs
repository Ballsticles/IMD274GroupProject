
using System.Collections.Generic;
using UnityEngine;


public class PlayerMotor : MonoBehaviour
{
    
    [Header ("References")]
    [SerializeField] Rigidbody rb;
    [SerializeField] GroundChecker groundCheck;
    [SerializeField] InputManager inputManager;



    [Header ("Settings")]
    [SerializeField] float movementSpeed = 6;
    [SerializeField] float rotationSpeed = 15;
    [SerializeField] float smoothTime = 0.2f;
    

    [Header ("Jump Settings")]
    [SerializeField] float jumpForce = 10;
    [SerializeField] float jumpDuration = 0.5f;
    [SerializeField] float jumpCooldown = 0f;
    [SerializeField] float jumpMaxHeight = 2f;
    [SerializeField] float gravityMultiplier = 3f;
    

 
    const float ZeroF = 0f;

    Transform cameraObject;

    float currentSpeed;
    float velocity;
    float jumpVelocity;

    Vector3 movement;

    List<Timer> timers;
    CountdownTimer jumpTimer;
    CountdownTimer jumpCooldownTimer;
    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        groundCheck = GetComponent<GroundChecker>();
        
        rb.freezeRotation = true;

        //setup timers
        jumpTimer = new CountdownTimer(jumpDuration);
        jumpCooldownTimer = new CountdownTimer(jumpCooldown);

        timers = new List<Timer>(2) {jumpTimer, jumpCooldownTimer};

        jumpTimer.onTimerStop += () => jumpCooldownTimer.Start();
    }

    void Start()
    {
        inputManager.EnablePlayerActions();
    }

    void OnEnable()
    {
        inputManager.Jump += OnJump;
    }

    void OnDisable()
    {
        inputManager.Jump -= OnJump;
    }

    public void OnJump(bool performed)
    {
        if (performed && !jumpTimer.IsRunning && !jumpCooldownTimer.IsRunning && groundCheck.isGrounded)
        {
            jumpTimer.Start();
        }else if (!performed && jumpTimer.IsRunning)
        {
            jumpTimer.Stop();
        }
    }

    private void Update()
    {
        
        movement = new Vector3(inputManager.Direction.x, 0f, inputManager.Direction.y);
        HandleTimers();
    }
    private void FixedUpdate()
    {
        HandleJump();
        HandleMovement();
    }

    void HandleTimers()
    {
        foreach (var timer in timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }


    private void HandleMovement()
    {
       
       
        // rotate movement direction to match camera rotation
        var adjustedDirection = Quaternion.AngleAxis(cameraObject.eulerAngles.y, Vector3.up) * movement;

        if (adjustedDirection.magnitude > ZeroF)
        {
            HandleRotation(adjustedDirection);
            HandleHorizontalMovement(adjustedDirection);
            SmoothSpeed(adjustedDirection.magnitude);
        }
        else
        {
            SmoothSpeed(ZeroF);
            rb.linearVelocity = new Vector3(ZeroF, rb.linearVelocity.y, ZeroF);
        }
    }
    void HandleHorizontalMovement(Vector3 adjustedDirection)
    {
        //move the player
        Vector3 velocity = adjustedDirection * movementSpeed * Time.deltaTime;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }
    private void HandleJump()
    {
        
        // If not jumping and grounded, keep jump velocity at 0
        if (!jumpTimer.IsRunning && groundCheck.isGrounded)
        {
            jumpVelocity = ZeroF;
            jumpTimer.Stop();
            return;
        }

        //if jumping or falling calculate velocity
        if (jumpTimer.IsRunning)
        {
            float launchPoint = 0.9f;
            if(jumpTimer.Progress > launchPoint)
            {
                //calculate the velocity required to reach the jump height using physics equation y = sqrt(2*gravity*height)
                jumpVelocity = Mathf.Sqrt(2 * jumpMaxHeight * Mathf.Abs(Physics.gravity.y));
            }
            else
            {
                //gradually apply less velocity as the jump progresses
                jumpVelocity += (1 - jumpTimer.Progress) * jumpForce * Time.fixedDeltaTime;
            }

        }
        else
        {
            //gravity Takes over
            jumpVelocity += Physics.gravity.y * gravityMultiplier * Time.fixedDeltaTime;

        }

        //apply velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpVelocity, rb.linearVelocity.z);

    }


    private void HandleRotation(Vector3 adjustedDirection)
    {
        //adjust rotation to match movement direction
        var targetRotation = Quaternion.LookRotation(adjustedDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
    }


    void SmoothSpeed(float value)
    {
        currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
    }
}
