using UnityEngine;

public class SBattleManager
{
    public void startBattle(SBattlePartyComponent partyOne, SBattlePartyComponent partyTwo)
    {
        Debug.Log($"Starting battle between : {partyOne.gameObject.name} and {partyTwo.gameObject.name}");
    }
}
