using System;
using System.Collections.Generic;
using UnityEngine;
public class SBattlePartyComponent : MonoBehaviour
{
    [SerializeField] SBattleCharacter[] mBattleCharactersPrefabs;

    List<SBattleCharacter> mBattleCharacters;

    IViewClient mOwnerViewClient;
    public event Action<SBattleCharacter> onBattleCharacterTakeTurn;

    [field: SerializeField] public int mPartyID { get; private set; } = 0; //for telling the difference between players and enemies
    private void Awake()
    {
        mOwnerViewClient = GetComponent<IViewClient>();
    }
    public void FinishPrep()
    {

    }
    public void UpdateView()
    {
        if (mOwnerViewClient is not null)
        {
            mOwnerViewClient.SetViewTarget(mBattleCharacters[0].transform);
            mOwnerViewClient.ResetViewAngle();
        }
    }
    public List<SBattleCharacter> GetBattleCharacters()
    {
        if(mBattleCharacters == null)
        {
            mBattleCharacters = new List<SBattleCharacter>();
            foreach(SBattleCharacter battlecharacter in mBattleCharactersPrefabs)
            {
                SBattleCharacter newBattleCharacter = Instantiate(battlecharacter);
                newBattleCharacter.onTurnStarted += CharacterInTurn;
                mBattleCharacters.Add(newBattleCharacter);
            }
        }
        return mBattleCharacters;
    }
    private void CharacterInTurn(SBattleCharacter character) //putting the camera behind that players and checking what the player is 
    {
        onBattleCharacterTakeTurn?.Invoke(character);
        if(mOwnerViewClient is not null && character)
        {
            mOwnerViewClient.SetViewTarget(character.transform);          
        }
    }
} 
