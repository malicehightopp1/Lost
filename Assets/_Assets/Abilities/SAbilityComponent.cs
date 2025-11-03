using System.Collections.Generic;
using UnityEngine;
public class SAbilityComponent : MonoBehaviour
{
    [SerializeField] private SAbility[] mInitialability;
    List<SAbility> mAbilities = new List<SAbility>();
    public int GetPartyId() //grabbing the party id for the abilities and attacks
    {
        return GetComponent<SBattleCharacter>().mPartyID;
    }
    private void Start()
    {
        foreach (SAbility initialAbility in mInitialability)
        {
            GiveAbility(initialAbility);
        }
    }
    private void GiveAbility(SAbility initialAbilityObject) //every ability is an instantiated one so every character will have ther own
    {
        SAbility newAbility = Instantiate(initialAbilityObject);
        newAbility.Init(this);
        mAbilities.Add(newAbility);
    }
    internal IEnumerable<SAbility> GetAbilities() //anything that ienumeable can be foreach throughed *makes it more flexable**
    {
        return mAbilities;
    }
}
