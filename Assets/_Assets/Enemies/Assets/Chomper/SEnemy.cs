using System;
using UnityEngine;

public class SEnemy : MonoBehaviour
{
    [SerializeField] private float mSightDistance = 5f;
    [SerializeField] private float mViewAngle = 30f;
    [SerializeField] private float mEyeHeight = 1.5f;
    GameObject mTarget
    { 
        get { return mTarget; }
        set { mTarget = value; }
    }
    void Update()
    {
        UpdatePlayerPerception();
    }
    private void UpdatePlayerPerception()
    {
        SPlayer player = SGameMode.mMainGameMode.mplayer;
        if (!player)
        {
            mTarget = null;
            return;
        }
        if(Vector3.Distance(player.transform.position, transform.position) > mSightDistance)
        {
            mTarget = null;
            return;
        }
        Vector3 playerDir = (player.transform.position - transform.position).normalized;
        if(Vector3.Angle(playerDir, transform.forward) > mViewAngle) //player must be in distance 
        {
            mTarget = null;
            return;
        }
        Vector3 eyeViewPoint = transform.position + Vector3.up * mEyeHeight;
        if(Physics.Raycast(eyeViewPoint, playerDir, out RaycastHit hitInfo, mSightDistance)) //checking to see if the raycast hit anything else besides player **if something is inbetween the player and enemy**
        {
            if(hitInfo.collider != player)
            {
                mTarget = null;
                return;
            }
        }
        mTarget = player.gameObject;
    }
    private void OnDrawGizmos()
    {
        Vector3 eyeViewPoint = transform.position + Vector3.up * mEyeHeight;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(eyeViewPoint, mSightDistance);

        Vector3 leftlineDir = Quaternion.AngleAxis(mViewAngle, Vector3.up) * transform.forward;
        Vector3 RightlineDir = Quaternion.AngleAxis(-mViewAngle, Vector3.up) * transform.forward;
        Gizmos.DrawLine(eyeViewPoint, eyeViewPoint + leftlineDir * mSightDistance);
        Gizmos.DrawLine(eyeViewPoint, eyeViewPoint + RightlineDir * mSightDistance);
    }
}
