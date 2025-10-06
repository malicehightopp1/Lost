using UnityEngine;

[ExecuteAlways]
public class SSpringArm : MonoBehaviour
{
    [Header("Default Variables")]
    [SerializeField] private Transform mAttachTransform;
    [SerializeField] private float mArmLength = 3f;
    [SerializeField] private float mCameraCollionOffset = 0.1f;
    [SerializeField] private LayerMask mCollisionLayerMask;
    void LateUpdate()
    {
        Vector3 endPOS = transform.position - transform.forward * mArmLength;
        if(Physics.Raycast(transform.position, -transform.forward, out RaycastHit hitinfo, mArmLength, mCollisionLayerMask)) //detecting if the camera collids with something 
        {
            endPOS = hitinfo.point + transform.forward * mCameraCollionOffset;
        }
        mAttachTransform.position = endPOS;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, mAttachTransform.position);
    }
}
