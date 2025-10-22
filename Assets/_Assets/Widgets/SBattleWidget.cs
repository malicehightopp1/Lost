using UnityEngine;
using UnityEngine.UI;
public class SBattleWidget : MonoBehaviour
{
    [SerializeField] private SCharacterControllerWidget mCharacterControllerWidget;
    [SerializeField] private LayoutGroup mAbilityistLayoutGroup;
    [SerializeField] private SAbilityWidget mabilityWidgetPrefab;
    public void SetCharacterContolTarget(SBattleCharacter battleCharacter)
    {
        foreach (Transform existing in mAbilityistLayoutGroup.transform)
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
    }
    private void AddabilityToAbilityList(SAbility ability)
    {
        SAbilityWidget newabilitywidget = Instantiate(mabilityWidgetPrefab , mAbilityistLayoutGroup.transform);
        newabilitywidget.SetAbility(ability);     
    }
}
