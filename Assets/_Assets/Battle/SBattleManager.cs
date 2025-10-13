using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SBattleManager
{
    List<SBattleSite> mBattleSites;
    public void startBattle(SBattlePartyComponent PlayerParty, SBattlePartyComponent EnemyParty)
    {
        if(mBattleSites == null)
        {
            mBattleSites = new List<SBattleSite>();
            mBattleSites.AddRange(GameObject.FindObjectsByType<SBattleSite>(FindObjectsSortMode.None)); //allows multiple types to be added **multiple objects**
        }
        Debug.Log($"Starting battle between : {PlayerParty.gameObject.name} and {EnemyParty.gameObject.name}");
        PrepParty(PlayerParty);
        PrepParty(EnemyParty);
    }
    private void PrepParty(SBattlePartyComponent Party)
    {
        SBattleSite partyBattleSite = mBattleSites.Find((battleSite) => { return !battleSite.IsPlayerSite;}); //takes a callable and converts to a boolean **internal forloop in simple terms** this one is checking for the one thats not the player
        if(Party.gameObject.CompareTag("Player"))
        {
            partyBattleSite = mBattleSites.Find((battleSite) => { return battleSite.IsPlayerSite; }); //takes a callable and converts to a boolean **internal forloop in simple terms** looking for the one that is the player
        }
        int i = 0; //index for the foreach loop below 
        foreach(SBattleCharacter partyBattleCharacter in Party.GetBattleCharacters()) 
        {
            partyBattleCharacter.transform.position = partyBattleSite.GetPOSForUnit(i); //putting the character in the right position
            partyBattleCharacter.transform.rotation = partyBattleSite.transform.rotation; //setting the rotation correctly
            i++;
        }
    }
}
