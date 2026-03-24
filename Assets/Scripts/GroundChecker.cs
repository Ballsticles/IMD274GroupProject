
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] float groundDistance = 0.08f;
    [SerializeField] float radius = 2;
    [SerializeField] float coyoteTime = 0.1f;
    [SerializeField] float coyoteProgress;
    [SerializeField] LayerMask groundLayers;
    bool groundCheck;
    public bool isGrounded {  get; private set; }
    CountdownTimer coyoteTimer;

    private void Update()
    {
        coyoteTimer.Tick(Time.deltaTime);
    }
    void Awake()
    {
        coyoteTimer = new CountdownTimer(coyoteTime);
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
        else if (!groundCheck && !coyoteTimer.IsFinished)
        {
            isGrounded = true;
            coyoteTimer.Start();
        }
        else if (!groundCheck && coyoteTimer.IsFinished)
        {
            isGrounded = false;
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
