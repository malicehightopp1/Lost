using UnityEngine;
using UnityEngine.UI;
public class SAbilityWidget : MonoBehaviour
{
    SAbility mAbility;
    Button mButton;
    private void Awake()
    {
        mButton = GetComponent<Button>();
        mButton.onClick.AddListener(ActivateAbility);
    }
    public void SetAbility(SAbility ability)
    {
        mAbility = ability;
    }
    void ActivateAbility()
    {
        mAbility.activateAbility();
    }
}
