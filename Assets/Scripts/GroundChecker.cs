using Unity.Cinemachine;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] float groundDistance = 0.08f;
    [SerializeField] float radius = 2;
    
    [SerializeField] LayerMask groundLayers;

    public bool isGrounded {  get; private set; }


    private void Update()
    {
        var position = transform.position + new Vector3(0f, groundDistance,0f);
        //isGrounded = Physics.SphereCast(transform.position, groundDistance,Vector3.down, out _, groundDistance, groundLayers);
        isGrounded = Physics.CheckSphere(position, radius, groundLayers);
        
    }

    private void OnDrawGizmos()
    {
        var position = transform.position + new Vector3(0f, groundDistance, 0f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(position,radius);
    }
}
