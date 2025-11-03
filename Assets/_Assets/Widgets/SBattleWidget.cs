using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NUnit.Framework;
using System.Collections.Generic;
public class SBattleWidget : MonoBehaviour
{
    List<SAbilityWidget> mAbilityWideget = new List<SAbilityWidget>();

    [SerializeField] private SCharacterControllerWidget mCharacterControllerWidget;
    [SerializeField] private LayoutGroup mAbilityistLayoutGroup;
    [SerializeField] private SAbilityWidget mabilityWidgetPrefab;
    public void SetCharacterContolTarget(SBattleCharacter battleCharacter)
    {
        foreach (Transform existing in mAbilityistLayoutGroup.transform) //destroying ui buttons so you only have the ones that you need **so it doesnt constantly spawn buttons**
        {
            Destroy(existing.gameObject);
        }
        mCharacterControllerWidget.gameObject.SetActive(true);
        mCharacterControllerWidget.SetBattleCharacter(battleCharacter);
        SAbilityComponent abilitycomponent = battleCharacter.GetAbilityComponet();
        if(abilitycomponent)
        {
            foreach (SAbility ability in abilitycomponent.GetAbilities())
            {
                AddabilityToAbilityList(ability);
            }
        }
        EventSystem.current.SetSelectedGameObject(mAbilityWideget[0].gameObject); //tells event system which ui to select on start **for controller to be able to use ui on start**
    }
    private void AddabilityToAbilityList(SAbility ability) //keeping a record of all the abilities
    {
        SAbilityWidget newabilitywidget = Instantiate(mabilityWidgetPrefab , mAbilityistLayoutGroup.transform);
        mAbilityWideget.Add(newabilitywidget);
        newabilitywidget.SetAbility(ability);     
    }
}
