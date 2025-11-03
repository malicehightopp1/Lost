using System;
using UnityEngine;
[RequireComponent(typeof(SAbilityComponent))]
public class SBattleCharacter : MonoBehaviour
{
    [Header("Changable variables")]
    [field: SerializeField] public float Speed { get; private set; } = 1; //actual speed for attack
    [field: SerializeField] public string Name { get; private set; } = "BattleCharacter"; //actual speed for attack
    [SerializeField] private GameObject mTurnIndicator;

    [Header("Cooldown variables")]
    public float mCooldownDuration => 1f / Speed;
    public float mCooldownTimeRemaining { get; private set; }

    [Header("reference")]
    SAbilityComponent mAbilityComponent;
    public event Action OnTurnFinished; //for when finished with turn
    public event Action<SBattleCharacter> onTurnStarted;
    public SAbilityComponent GetAbilityComponet()
    {
        return mAbilityComponent;
    }
    private void Awake()
    {
        mCooldownTimeRemaining = mCooldownDuration;
        mTurnIndicator.SetActive(false);

        mAbilityComponent = GetComponent<SAbilityComponent>();
    }         
    public void TakeTurn() //call at start
    {
        //Invoke("FinishTurn", 1);
        mTurnIndicator.SetActive(true);
        onTurnStarted?.Invoke(this);
        mCooldownTimeRemaining = mCooldownDuration;
    }
    public void FinishTurn()
    {
        mTurnIndicator.SetActive(false);
        OnTurnFinished?.Invoke();
    }
    internal void AdvanceCooldown(float advanceTime)
    {
        mCooldownTimeRemaining -= advanceTime;
    }
    //we want when its a characters turn that the turn indicator gets set active over there head for proof of turn
}
