using UnityEngine;
public class SBattleSite : MonoBehaviour
{
    [SerializeField] private float mSiteRadius;
    [SerializeField , Range (0, 5)] private int mSiteCapacity;

    [SerializeField] bool mIsPlayerSite = false;
    public bool IsPlayerSite => mIsPlayerSite;
    //zero index, first unit have an index of 0;
    public Vector3 GetPOSForUnit(int index)
    {
        //edge case
        if(mSiteCapacity <= 1) //starting point is center
        {
            return transform.position;
        }
        // D / (n - 1) - diameter / (mSiteCapacity - 1)
        float gap = mSiteRadius * 2 / (mSiteCapacity - 1); //calculating the gap by taking the diameter then dividing by the number of gaps between units
        Vector3 StartingPoint = transform.position - transform.right * mSiteRadius; //making the starting point 

        return StartingPoint + index * gap * transform.right; //offsetting/making the gaps decided by how many units there are in the index
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = mIsPlayerSite ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, mSiteRadius);
        for (int i = 0; i < mSiteCapacity; ++i)
        {
            Gizmos.DrawSphere(GetPOSForUnit(i), 0.5f); 
        }
    }
}
