using System.Collections.Generic;
using UnityEngine;
public class SBattlePartyComponent : MonoBehaviour
{
    [SerializeField] SBattleCharacter[] mBattleCharactersPrefabs;

    List<SBattleCharacter> mBattleCharacters;

    IViewClient mOwnerViewClient;
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
                newBattleCharacter.onTurnStarted += ChangeViewTo;
                mBattleCharacters.Add(newBattleCharacter);
            }
        }
        return mBattleCharacters;
    }
    private void ChangeViewTo(SBattleCharacter character)
    {
        if(mOwnerViewClient is not null && character)
        {
            mOwnerViewClient.SetViewTarget(character.transform);
        }
    }
} 
