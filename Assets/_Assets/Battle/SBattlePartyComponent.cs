using System.Collections.Generic;
using UnityEngine;

public class SBattlePartyComponent : MonoBehaviour
{
    [SerializeField] SBattleCharacter[] mBattleCharactersPrefabs;

    List<SBattleCharacter> mBattleCharacters;
    public List<SBattleCharacter> GetBattleCharacters()
    {
        if(mBattleCharacters == null)
        {
            mBattleCharacters = new List<SBattleCharacter>();
            foreach(SBattleCharacter battlecharacter in mBattleCharactersPrefabs)
            {
                mBattleCharacters.Add(Instantiate(battlecharacter));
            }
        }
        return mBattleCharacters;
    }
} 
