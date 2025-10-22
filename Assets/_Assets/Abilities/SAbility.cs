using System;
using UnityEngine;
public abstract class SAbility : ScriptableObject //abstract can only be used as parent class
{
    [field: SerializeField] public string mAbilityName { get; private set; }
    private SAbilityComponent mOwningAbilityComponent;
    internal void Init(SAbilityComponent newAbility)
    {
        mOwningAbilityComponent = newAbility;
    }
    public virtual void activateAbility() //this waits till its the active players turn to connect the scripts **gives errors if you attempt to click the button beforehand**
    {
        Debug.Log($"Hello world");
    }
}
