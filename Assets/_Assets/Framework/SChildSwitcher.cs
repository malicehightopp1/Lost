using System.Collections.Generic;
using UnityEngine;
public class SChildSwitcher : MonoBehaviour
{
    List<GameObject> mChildGameobject = new List<GameObject>();
    int mCurrentActiveChildIndex = 0;
    private void Awake()
    {
        foreach(Transform childtransform in transform) 
        {
            mChildGameobject.Add(childtransform.gameObject);
        }
        SetActiveChildByIndex(mCurrentActiveChildIndex);
    }
    public void SetActiveChild(GameObject childToSwitchTo)
    {
        int childindex = mChildGameobject.FindIndex((x) => {return childToSwitchTo == x;});
        SetActiveChildByIndex(childindex);
    }
    private void SetActiveChildByIndex(int newActiveChildIndex) //gurettees that only one is active
    {
        if (newActiveChildIndex < 0 || newActiveChildIndex >= mChildGameobject.Count)
        {
            return;
        }
        foreach (GameObject childGameobject in mChildGameobject)
        {
            childGameobject.SetActive(false);
        }

        mCurrentActiveChildIndex = newActiveChildIndex;
        mChildGameobject[mCurrentActiveChildIndex].SetActive(true);
    }
}
