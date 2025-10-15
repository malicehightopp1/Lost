using System;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class SPlayer : MonoBehaviour, IViewClient
{
    [Header("UI")]
    [SerializeField] private SGameplayWidget mGameplayWidgetPrefab;
    SGameplayWidget mGameplayWidget;

    [Header("Player references")]
    private PlayerInputAction mPlayerInputActions;
    private MovementController mMovementController;

    [Header("Battle")]
    private BattleState battleState;
    private SBattlePartyComponent mBattlePartyComponent;

    [Header("Camera")]
    [SerializeField] private SCameraRig mCameraRigPrefab;
    SCameraRig mCamerRig;
    void Awake()
    {
        mCamerRig = Instantiate(mCameraRigPrefab);
        mCamerRig.SetFolowTransform(transform);
        mPlayerInputActions = new PlayerInputAction();
        mGameplayWidget = Instantiate(mGameplayWidgetPrefab);

        mMovementController = GetComponent<MovementController>();
        mPlayerInputActions.Gameplay.Jump.performed +=  mMovementController.PerformJump; //detects when jump input happens
        mPlayerInputActions.Gameplay.Move.performed += mMovementController.HandleMoveInput; //detects when move input happens
        mPlayerInputActions.Gameplay.Move.canceled += mMovementController.HandleMoveInput;  //detects when movements stop

        mPlayerInputActions.Gameplay.Look.performed += (context) => mCamerRig.SetLookInput(context.ReadValue<Vector2>());//=> - lambda - simpifying so not having to make a function - acts like function but without actually making it 
        mPlayerInputActions.Gameplay.Look.canceled += (context) => mCamerRig.SetLookInput(context.ReadValue<Vector2>());

        mBattlePartyComponent = GetComponent<SBattlePartyComponent>();
    }
    private void OnEnable()
    {
        mPlayerInputActions.Enable(); //enabled when script is eneabled
    } 
    private void OnDisable()
    {
        mPlayerInputActions.Disable();//disabled when script is disabled
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == gameObject)
        {
            return; 
        }
        SBattlePartyComponent otherBattleComponent = other.GetComponent<SBattlePartyComponent>();
        if(otherBattleComponent && !IsInBattle())
        {
            SGameMode.mMainGameMode.mBattleManager.startBattle(mBattlePartyComponent, otherBattleComponent);
            SwitchToBattleMode(BattleState.InBattle);
        }
    }
    private bool IsInBattle()
    {
        return battleState == BattleState.InBattle;
    }
    private void SwitchToBattleMode(BattleState battleState)
    {
        if (battleState == BattleState.InBattle)
        {
            mPlayerInputActions.Gameplay.Disable();
        }
        if (battleState == BattleState.Roaming)
        {
            mPlayerInputActions.Gameplay.Enable();
        }
        mGameplayWidget.DipToBlack(1, 1, DippedToBlack); //when you get to black call to the function of dipped to black **not calling the function more or less just talking to it**
    }
    private void DippedToBlack() 
    {
        Debug.Log($"Dipped to black called");
        mBattlePartyComponent.UpdateView();
    }
    public void SetViewTarget(Transform viewTarget)
    {
        mCamerRig.SetFolowTransform(viewTarget);
        mCamerRig.transform.rotation = viewTarget.transform.rotation;
    }
    public void ResetViewAngle()
    {
        mCamerRig.ResetViewAngle();   
    }
}