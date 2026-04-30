
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] float groundDistance = 0.08f;
    [SerializeField] float radius = 2;
    [SerializeField] float coyoteTime = 1f;
    [SerializeField] float coyoteProgress;
    [SerializeField] LayerMask groundLayers;
    bool groundCheck;
    public bool isGrounded {  get; private set; }
    public CountdownTimer coyoteTimer;
  void Awake()
    {
        coyoteTimer = new CountdownTimer(coyoteTime);
        coyoteTimer.onTimerStop += () => isGrounded = false;
        
    }
    private void Update()
    {
        coyoteTimer.Tick(Time.deltaTime);
    }
  
    private void FixedUpdate()
    {

    }
    public void CheckForGround()
    {
        var position = transform.position + new Vector3(0f, groundDistance, 0f);
        //isGrounded = Physics.SphereCast(transform.position, groundDistance,Vector3.down, out _, groundDistance, groundLayers);
        groundCheck = Physics.CheckSphere(position, radius, groundLayers);
        if (groundCheck)
        {
            isGrounded = true;
        }
        else if (!groundCheck && !coyoteTimer.IsRunning)
        {
            coyoteTimer.Start();
        }
        
 
        coyoteProgress = coyoteTimer.Progress;
    }
    private void OnDrawGizmos()
    {
        var position = transform.position + new Vector3(0f, groundDistance, 0f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(position,radius);
    }
}
