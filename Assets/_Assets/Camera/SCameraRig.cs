using UnityEngine;

public class SCameraRig : MonoBehaviour
{
    Transform mFollowTransform;
    [SerializeField] float mHeightOffset = 0.5f;
    [SerializeField] float mFollowLerpRate = 20f;

    [SerializeField] Transform mYawTransform;
    [SerializeField] Transform mPitchTransform;

    [SerializeField] float mPitchmin = -89f;
    [SerializeField] float mPitchMax = 89f;

    float mPitch;

    [SerializeField] float mRotationRate;

    Vector2 mLookIput;
    public void SetLookInput(Vector2 lookInput)
    {
        mLookIput = lookInput;
    }
    public void SetFolowTransform(Transform followTransform)
    {
        mFollowTransform = followTransform;
    }
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, mFollowTransform.position + mHeightOffset * Vector3.up, mFollowLerpRate * Time.deltaTime);

        mYawTransform.rotation *= Quaternion.AngleAxis(mLookIput.x * mRotationRate * Time.deltaTime, Vector2.up); //x axis rotation

        mPitch = mPitch + mRotationRate * Time.deltaTime * mLookIput.y; //y axis rotation
        mPitch = Mathf.Clamp(mPitch, mPitchmin, mPitchMax);
        mPitchTransform.localEulerAngles = new Vector3(mPitch, 0, 0);
    }
}
