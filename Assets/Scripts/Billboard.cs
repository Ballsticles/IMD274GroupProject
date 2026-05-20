using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private BillboardType billboardType;
    public enum BillboardType { lookAtCamera, CameraForward}




    private void LateUpdate()
    {
        switch (billboardType) 
        {
            case BillboardType.lookAtCamera:
                transform.LookAt(Camera.main.transform.position, Vector3.up); break;
            case BillboardType.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            default:
                break;
        }
    }

}
