using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SAbilityWidget : MonoBehaviour
{
    SAbility mAbility;
    Button mButton;
    [SerializeField]TextMeshProUGUI mButtonText;
    private void Awake()
    {
        mButton = GetComponent<Button>();
        mButton.onClick.AddListener(ActivateAbility);
    }
    public void SetAbility(SAbility ability)
    {
        mAbility = ability;
        mButtonText.text = ability.mAbilityName;
    }
    void ActivateAbility()
    {
        mAbility.activateAbility();
    }
}
