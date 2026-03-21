using UnityEngine;

public class GrappleScript : MonoBehaviour
{
    [Header("References")]
    public InputManager inputs;
    public LineRenderer lr;
    public Transform outPoint, cam, player;
    public LayerMask whatIsGrappleable;


    [Header("Swinging")]
    public bool isSwinging;
    public float maxSwingDistance = 25f;
    private Vector3 swingPoint;
    private SpringJoint joint;


    private Vector3 currentGrapplePosition;

    [Header("Prediction")]
    public RaycastHit predictionHit;
    public float predictionSphereCastRadius;
    public Transform predictionPoint;

    [Header("RopeStuff")]
    public float minDistanceMult = 0.25f;
    public float maxDistanceMult = 0.8f;
    public float jointSpring = 4.5f;
    public float jointDampen = 7f;
    public float jointMassScale = 4.5f;
   
 
    private void Awake()
    {
        
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        cam = Camera.main.transform;
        player = gameObject.transform;

    }


    void OnEnable()
    {
        inputs.Grab += OnSwing;
    }

    void OnDisable()
    {
        inputs.Grab -= OnSwing;
    }
    // Update is called once per frame
    void Update()
    {
        CheckForSwingPoints();

    }
    private void LateUpdate()
    {
        DrawRope();
    }


    public void OnSwing(bool performed)
    {
        if(performed)
        {
            StartSwing();

        }
        else if (!performed && isSwinging) 
        {
            StopSwing();
        }

    }
    private void StartSwing()
    {
        

        if(predictionHit.point == Vector3.zero) return;

        // deactovate active grapple
        isSwinging = true;

        swingPoint = predictionHit.point;
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = swingPoint;

        float distanceFromPoint = Vector3.Distance(player.position, swingPoint);


        //the distance grapple will try to keep from grapple point
        joint.maxDistance = distanceFromPoint * maxDistanceMult;
        joint.minDistance = distanceFromPoint * minDistanceMult;


        //customize values
        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;
        lr.enabled = true;
        lr.positionCount = 2;
        currentGrapplePosition = outPoint.position;

    }
    
    private void StopSwing()
    {
        lr.positionCount = 0;
        lr.enabled = false;
        Destroy(joint);
        isSwinging = false;
    }

    void DrawRope()
    {
        if (!joint) return;

        lr.SetPosition(0, outPoint.position);
        lr.SetPosition(1, swingPoint);
    }

    private void CheckForSwingPoints()
    {
        if (joint != null) return;
        RaycastHit sphereCastHit;
        Physics.SphereCast(cam.position,predictionSphereCastRadius, cam.forward, out sphereCastHit, maxSwingDistance, whatIsGrappleable);

        RaycastHit raycastHit;
        Physics.Raycast(cam.position,cam.forward, out  raycastHit, maxSwingDistance,whatIsGrappleable);


        Vector3 realHitPoint;
        //Direct hit 
        if(raycastHit.point != Vector3.zero) realHitPoint = raycastHit.point;
        //indirect hit
        else if (sphereCastHit.point != Vector3.zero) realHitPoint = sphereCastHit.point;
        //miss
        else realHitPoint = Vector3.zero;

        //hitPoint found
        if(realHitPoint != Vector3.zero)
        {
            predictionPoint.gameObject.SetActive(true);
            predictionPoint.parent.position = realHitPoint;


        }

        // realHitPOint not found
        else
        {
            predictionPoint.gameObject.SetActive(false);
        }

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;
        


    }


}
