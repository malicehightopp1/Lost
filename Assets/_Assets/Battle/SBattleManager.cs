using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SBattleManager : MonoBehaviour 
{
    List<SBattleSite> mBattleSites;
    List<SBattleCharacter> mBattleCharacter = new List<SBattleCharacter>();
    private PlayerInputAction mAction;

    IViewClient mOwnerViewClient;
    private BattleState mBattleStates;
    Queue<SBattleCharacter> mFirstBattleCharacterQueue = new Queue<SBattleCharacter>(); //queue every character in to a queue then take them out one by one
    private void Awake()
    {
        mOwnerViewClient = GetComponent<IViewClient>();
    }
    public void startBattle(SBattlePartyComponent PlayerParty, SBattlePartyComponent EnemyParty)
    {
        mBattleCharacter.Clear(); //clearing before new battle
        if(mBattleSites == null)
        {
            mBattleSites = new List<SBattleSite>();
            mBattleSites.AddRange(GameObject.FindObjectsByType<SBattleSite>(FindObjectsSortMode.None)); //allows multiple types to be added **multiple objects**
        }
        Debug.Log($"Starting battle between : {PlayerParty.gameObject.name} and {EnemyParty.gameObject.name}");
        PrepParty(PlayerParty);
        PrepParty(EnemyParty);
        StartCoroutine(StartTurns());
    }
    IEnumerator StartTurns()
    {
        //TODO: refractor to not hard code delay
        yield return new WaitForSeconds(2);
        UpdateTurnOrder();
        mFirstBattleCharacterQueue = new Queue<SBattleCharacter>(mBattleCharacter); //adding the characters to the queue in the oder there in by default
        ProcessFirstRound();
    }
    private void ProcessFirstRound() //removes characters from battle characters list
    {
        if(mFirstBattleCharacterQueue.TryDequeue(out SBattleCharacter nextBattleCharacter)) //works with seperate container
        {
            if(mBattleCharacter.Contains(nextBattleCharacter))
            {
                nextBattleCharacter.TakeTurn();
            }
            else
            {
                ProcessFirstRound();
            }
            return;
        }
        foreach(SBattleCharacter battlecharacter in mBattleCharacter)
        {
            battlecharacter.OnTurnFinished -= ProcessFirstRound;
            battlecharacter.OnTurnFinished += NextTurn;
        }
        NextTurn();
    }
    private void NextTurn() 
    {
        UpdateTurnOrder();

        float advanceTime = mBattleCharacter[0].mCooldownTimeRemaining;
        foreach(SBattleCharacter character in mBattleCharacter) //looking through the list 
        {
            character.AdvanceCooldown(advanceTime);
        }

        SBattleCharacter nextinturn = mBattleCharacter[0];
        mBattleCharacter[0].TakeTurn();

        mBattleCharacter.Remove(nextinturn);
        mBattleCharacter.Add(nextinturn);
    }
    private void UpdateTurnOrder()
    {
        mBattleCharacter = mBattleCharacter.OrderBy((batteCharacter) => { return batteCharacter.mCooldownTimeRemaining; }).ThenBy((batteCharacter) => { return 1/batteCharacter.Speed;}).ToList(); //sorts through based on our criteria
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
            partyBattleCharacter.OnTurnFinished += ProcessFirstRound;
            mBattleCharacter.Add(partyBattleCharacter);
            i++;
        }
        Party.FinishPrep();
    }
}
