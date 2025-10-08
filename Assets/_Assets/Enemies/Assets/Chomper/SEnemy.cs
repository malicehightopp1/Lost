using System;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;

public class SEnemy : MonoBehaviour
{
    [SerializeField] private float mSightDistance = 5f;
    [SerializeField] private float mViewAngle = 30f;
    [SerializeField] private float mEyeHeight = 1.5f;
    [SerializeField] private float mAlwaysAwareDistance = 1f;
    BehaviorGraphAgent mBehaviorGraphAgent;
    GameObject mTarget;
    GameObject Target
    { 
        get { return mTarget; }
        set 
        { 
            if(Target == value) //not updating when we dont have too
            {
                return;
            }
            if(value == null) //checking if the enemy lost track of the player
            {
                mBehaviorGraphAgent.BlackboardReference.SetVariableValue("HasLastSeenPOS", true);
                mBehaviorGraphAgent.BlackboardReference.SetVariableValue("TargetLastPOS", mTarget.transform.position);
            }
            mTarget = value;
            mBehaviorGraphAgent.BlackboardReference.SetVariableValue("Target", mTarget); //setting target to be target
        } 
    }
    private void Awake()
    {
        mBehaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
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
            Target = null;
            return;
        }
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if(distanceToPlayer <= mAlwaysAwareDistance) //if player is in this range the enemy can always see you
        {
            Target = player.gameObject;
            return;
        }
        Vector3 playerDir = (player.transform.position - transform.position).normalized;
        if(Vector3.Angle(playerDir, transform.forward) > mViewAngle) //player must be in distance 
        {
            Target = null;
            return;
        }
        Vector3 eyeViewPoint = transform.position + Vector3.up * mEyeHeight;
        if(Physics.Raycast(eyeViewPoint, playerDir, out RaycastHit hitInfo, mSightDistance)) //checking to see if the raycast hit anything else besides player **if something is inbetween the player and enemy**
        {
            if(hitInfo.collider.gameObject != player.gameObject)
            {
                Target = null;
                return;
            }
        }
        Target = player.gameObject;
    }
    private void OnDrawGizmos()
    {
        Vector3 eyeViewPoint = transform.position + Vector3.up * mEyeHeight;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(eyeViewPoint, mSightDistance);
        Gizmos.DrawWireSphere(eyeViewPoint, mAlwaysAwareDistance);

        Vector3 leftlineDir = Quaternion.AngleAxis(mViewAngle, Vector3.up) * transform.forward;
        Vector3 RightlineDir = Quaternion.AngleAxis(-mViewAngle, Vector3.up) * transform.forward;
        Gizmos.DrawLine(eyeViewPoint, eyeViewPoint + leftlineDir * mSightDistance);
        Gizmos.DrawLine(eyeViewPoint, eyeViewPoint + RightlineDir * mSightDistance);
        if(Target)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Target.transform.position);
            Gizmos.DrawWireSphere(Target.transform.position, 0.5f);
        }
    }
}
