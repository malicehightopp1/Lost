using System;
using UnityEngine;

public class SBattleCharacter : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; private set; } = 1; //actual speed for attack
    [SerializeField] private GameObject mTurnIndicator;
    public float mCooldownDuration => 1f / Speed;
    public float mCooldownTimeRemaining { get; private set; }

    public event Action OnTurnFinished; //for when finished with turn
    public event Action<SBattleCharacter> onTurnStarted;
    private void Awake()
    {
        mCooldownTimeRemaining = mCooldownDuration;
        mTurnIndicator.SetActive(false);
    }
    public void TakeTurn() //call at start
    {
        Invoke("FinishTurn", 1);
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
