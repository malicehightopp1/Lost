using System;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class SPlayer : MonoBehaviour
{
    [SerializeField] private SCameraRig mCameraRigPrefab;

    private BattleState battleState;

    private PlayerInputAction mPlayerInputActions;

    private MovementController mMovementController;
    SCameraRig mCamerRig;
    void Awake()
    {
        mCamerRig = Instantiate(mCameraRigPrefab);
        mCamerRig.SetFolowTransform(transform);
        mPlayerInputActions = new PlayerInputAction();

        mMovementController = GetComponent<MovementController>();
        mPlayerInputActions.Gameplay.Jump.performed +=  mMovementController.PerformJump; //detects when jump input happens
        mPlayerInputActions.Gameplay.Move.performed += mMovementController.HandleMoveInput; //detects when move input happens
        mPlayerInputActions.Gameplay.Move.canceled += mMovementController.HandleMoveInput;  //detects when movements stop

        mPlayerInputActions.Gameplay.Look.performed += (context) => mCamerRig.SetLookInput(context.ReadValue<Vector2>()); //=> - lambda - simpifying so not having to make a function - acts like function but without actually making it 
        mPlayerInputActions.Gameplay.Look.canceled += (context) => mCamerRig.SetLookInput(context.ReadValue<Vector2>());
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
            //SGameMode.mMainGameMode.mBattleManager.startBattle(mBattlePartyComponent, otherBattleComponent);
        }
    }

    private bool IsInBattle()
    {
        return battleState == BattleState.InBattle;
    }
}
