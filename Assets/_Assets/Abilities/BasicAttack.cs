using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/basic attack")]
public class BasicAttack : SAbility
{
    public override void activateAbility()
    {
        base.activateAbility();
        int partyId = OwningAbilityComponent.GetPartyId();
        List<SBattleCharacter> targets = SGameMode.mMainGameMode.mBattleManager.GetTargetsForTeam(partyId, true);
        foreach (SBattleCharacter battlecharacter in targets)
        {
            Debug.Log($"found target: {battlecharacter.gameObject.name}");
        }
    }
}
