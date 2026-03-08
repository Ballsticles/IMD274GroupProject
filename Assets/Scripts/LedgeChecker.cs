using Unity.Cinemachine;
using UnityEngine;

public class LedgeChecker : MonoBehaviour
{

    [Header("References")]
    [SerializeField] LayerMask layerMask;
    [SerializeField] Rigidbody rb;
    [Header("Line Check Floats")]
    
    [SerializeField] float downStartOffset = 1.5f;
    [SerializeField] float downEndOffset = 0.7f;
    [SerializeField] float fwdRange = 1;

    [Header("Hang Position Floats")]

    [SerializeField] float downOffset = -0.5f;
    [SerializeField] float forwardOffset = -0.1f;

    public bool onLedge;
    public Vector3 hangPos { get; private set; } 
    public Vector3 hangRot { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rb.linearVelocity.y < 0 && !onLedge)
        {
            //cast a line parallel to the player to check for an object
            
            RaycastHit downHit;
            Vector3 lineDownStart = (transform.position + (Vector3.up * downStartOffset) ) + transform.forward;
            Vector3 lineDownEnd = (transform.position + (Vector3.down * downEndOffset) ) + transform.forward;
            Physics.Linecast(lineDownStart, lineDownEnd, out downHit , layerMask);
            Debug.DrawLine(lineDownStart, lineDownEnd);


            if(downHit.collider != null)
            {
                //cast a perpendicular line at the height of the object to check for the ledge
                
                RaycastHit fwdHit;
                Vector3 lineFwdStart = new Vector3(transform.position.x, downHit.point.y - 0.1f, transform.position.z);
                Vector3 lineFwdEnd = new Vector3(transform.position.x, downHit.point.y -0.1f, transform.position.z) + transform.forward * fwdRange;
                Physics.Linecast(lineFwdStart, lineFwdEnd, out fwdHit , layerMask);
                Debug.DrawLine (lineFwdStart, lineFwdEnd);

                if (fwdHit.collider != null)
                {
                   

                    onLedge = true;
                    //set the position that the player should hang at 
                    hangPos = new Vector3(fwdHit.point.x, downHit.point.y, fwdHit.point.z);
                    Vector3 offset = transform.forward * forwardOffset + transform.up * downOffset;
                    hangPos += offset;
                    hangRot = -fwdHit.normal;
                }
            }

        }
    }
}
