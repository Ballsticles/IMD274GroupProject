
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class PlayerMotor : MonoBehaviour
{
    
    [Header ("References")]
    [SerializeField] Rigidbody rb;
    [SerializeField] GroundChecker groundCheck;
    [SerializeField] LedgeChecker ledgeChecker;
    [SerializeField] InputManager inputManager;
    [SerializeField] Animator animator;



    [Header ("Settings")]
    [SerializeField] float movementSpeed = 6;
    [SerializeField] float rotationSpeed = 15;
    [SerializeField] float smoothTime = 0.2f;
    

    [Header ("Jump Settings")]
    [SerializeField] float jumpForce = 10;
    [SerializeField] float jumpDuration = 0.5f;
    [SerializeField] float jumpCooldown = 0f;
    public float gravityMultiplier = 3f;
    

    [Header("Dive Settings")]
    [SerializeField] float diveForce = 10;   
    [SerializeField] float diveDuration = 0.5f;
    [SerializeField] float diveCooldown = .3f;
 
    [SerializeField] bool hasDive;
  

    GrappleScript grapple;
    public bool swinging; 
 
    const float ZeroF = 0f;

    Transform cameraObject;

    float currentSpeed;
    float velocity;
    float jumpVelocity;
    float diveVelocity = 1f;


    Vector3 movement;

    StateMachine stateMachine;
    public string currentState;
    //animator parameters
    static readonly int Speed = Animator.StringToHash("Speed");
    static readonly int Grounded = Animator.StringToHash("Grounded");
    static readonly int onLedge = Animator.StringToHash("Ledge");


    

    List<Timer> timers;
    CountdownTimer jumpTimer;
    CountdownTimer jumpCooldownTimer;

    CountdownTimer diveTimer;
    CountdownTimer diveCooldownTimer;


    bool unlockedDoubleJump = true;

    private void Awake()
    {
        grapple = GetComponent<GrappleScript>();
        rb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        groundCheck = GetComponent<GroundChecker>();
        ledgeChecker = GetComponent<LedgeChecker>();
        
        rb.freezeRotation = true;

        //setup timers
        jumpTimer = new CountdownTimer(jumpDuration);
        jumpCooldownTimer = new CountdownTimer(jumpCooldown);

        jumpTimer.onTimerStart += () => jumpVelocity = jumpForce;
        jumpTimer.onTimerStop += () => jumpCooldownTimer.Start();

        diveTimer = new CountdownTimer(diveDuration);
        diveCooldownTimer = new CountdownTimer(diveCooldown);

        diveTimer.onTimerStart -= () => diveVelocity = diveForce;
        diveTimer.onTimerStop += () => {
            diveVelocity = 1f;
            diveCooldownTimer.Start();
        };

        timers = new List<Timer>(4) {jumpTimer, jumpCooldownTimer , diveTimer , diveCooldownTimer};

        //State Machine
        stateMachine = new StateMachine();

        // Declare states
        var locomotionState = new LocomotionState(this, animator, groundCheck);
        var jumpState = new JumpState(this, animator, groundCheck);
        var swingState = new SwingState(this, animator, groundCheck);
        var fallState = new FallState(this, animator, groundCheck);
        var ledgeState = new LedgeState(this, animator, groundCheck);
        var diveState = new DiveState(this,animator,groundCheck);
        // Define transitions
        //jump transitions
        At(locomotionState, jumpState, new FuncPredicate(() => jumpTimer.IsRunning));
        At(fallState, jumpState, new FuncPredicate(() => jumpTimer.IsRunning && hasDive));
        At(ledgeState, jumpState, new FuncPredicate(() => jumpTimer.IsRunning));
        
        //locomotionState Transitions
        At(jumpState, locomotionState, new FuncPredicate(() => groundCheck.isGrounded && !jumpTimer.IsRunning && !ledgeChecker.onLedge));
        At(swingState, locomotionState, new FuncPredicate(() => groundCheck.isGrounded));
        At(fallState, locomotionState, new FuncPredicate(() => groundCheck.isGrounded && !ledgeChecker.onLedge));

        //fall state transitions
        Any(fallState, new FuncPredicate(() => !groundCheck.isGrounded && !jumpTimer.IsRunning && !grapple.isSwinging && !ledgeChecker.onLedge));
        
        
        //swingState Transitions
        Any(swingState, new FuncPredicate(() => grapple.isSwinging));
        

        //ledgeState Transitions
        Any(ledgeState, new FuncPredicate(() => ledgeChecker.onLedge && !jumpTimer.IsRunning));

        Any(diveState, new FuncPredicate(() => hasDive && diveTimer.IsRunning));

        // Set Initial State

        stateMachine.SetState(locomotionState);
        hasDive = true;
    }

    void At(IStates from, IStates to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    void Any(IStates to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

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
        if (performed && !jumpTimer.IsRunning && !jumpCooldownTimer.IsRunning && (groundCheck.isGrounded || ledgeChecker.onLedge))
        {
            animator.SetTrigger("Jump");
            jumpTimer.Start();
        }
        else if (!performed && jumpTimer.IsRunning || performed && jumpTimer.IsFinished)
        {
            jumpTimer.Stop();
            animator.ResetTrigger("Jump");
        }
        animator.ResetTrigger("Jump");
        if (performed && !jumpTimer.IsRunning && hasDive && !groundCheck.isGrounded)
        {
            
            animator.SetTrigger("DoubleJump");
            
            diveTimer.Start();
            hasDive = false;
        }
    }

    private void Update()
    {
        if(jumpTimer.IsRunning || groundCheck.isGrounded)
        {
            swinging = false;
        }

        if (unlockedDoubleJump)
        {
            if(!hasDive)
            {
                if(groundCheck.isGrounded)
                {

                    hasDive = true;
                }
            }
        }
        
        movement = new Vector3(inputManager.Direction.x, 0f, inputManager.Direction.y);

        stateMachine.Update();

        HandleTimers();
        UpdateAnimator();
        currentState = stateMachine.current.State.ToString();
    }
    private void FixedUpdate()
    {
        HandleLedge();
        stateMachine.FixedUpdate();
        
    }

    void UpdateAnimator()
    {
        animator.SetFloat(Speed, currentSpeed);
        animator.SetBool(Grounded, groundCheck.isGrounded);
        animator.SetBool(onLedge, ledgeChecker.onLedge);
       
        animator.SetBool("Swinging", grapple.isSwinging);
    }

    void HandleTimers()
    {
        foreach (var timer in timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }


    public void HandleMovement()
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
        Vector3 velocity = adjustedDirection * movementSpeed * diveVelocity * Time.deltaTime;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }


    public void HandleLedge()
    {
        if (ledgeChecker.onLedge)
        {
            
            transform.position = ledgeChecker.hangPos;
            transform.rotation = Quaternion.LookRotation(ledgeChecker.hangRot);
            rb.useGravity = false;

            if (jumpTimer.IsRunning)
            {
                ledgeChecker.onLedge = false;
                rb.useGravity = true;
            }
        }
        else
        {
            rb.useGravity = true;
            ledgeChecker.onLedge = false;

        }
    }
    public void HandleJump()
    {
        
        // If not jumping and grounded, keep jump velocity at 0
        if (!jumpTimer.IsRunning && groundCheck.isGrounded || ledgeChecker.onLedge || swinging)
        {
            jumpVelocity = ZeroF;
            jumpTimer.Stop();
            return;
        }

        //if jumping or falling calculate velocity
        if (!jumpTimer.IsRunning)
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
