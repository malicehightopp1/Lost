using TMPro;
using UnityEngine;
public class SCharacterControllerWidget : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mcharacterNameText;
    internal void SetBattleCharacter(SBattleCharacter battleCharacter)
    {
        mcharacterNameText.SetText(battleCharacter.Name);
    }
}
