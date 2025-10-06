using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))] //to function must have this component
public class MovementController : MonoBehaviour
{
    [Header("Set variables")] 
    [SerializeField] private float mJumpSpeed = 15f;
    [SerializeField] private float mMaxFallSpeed = 50f;
    [SerializeField] private float mMaxMoveSpeed = 5f;
    [SerializeField] private float mGroundMoveSpeedAcceleration = 50f;
    [SerializeField] private float mAirMoveSpeedAcceleration = 5f;
    private bool mShouldJump;
    private bool mIsInAir;

    [SerializeField] private float mAirCheckRadius = 0.5f;
    [SerializeField] private LayerMask mAirCheckLayerMask = 1;

    [SerializeField] float mTurnLerpRate = 40;
    [Header("References")]
    private CharacterController mCharacterController;
    private Animator mAnimator;

    [Header("Vectorss for Movement")]
    private Vector3 mVerticalVelocity;
    private Vector2 mMoveInput;
    private Vector3 mHorizontalVelocity;//speed

    private void Awake()
    {
        mCharacterController = GetComponent<CharacterController>();
        mAnimator = GetComponent<Animator>();
    }
    public void HandleMoveInput(InputAction.CallbackContext context) //handling player movement
    {
        mMoveInput = context.ReadValue<Vector2>();
        //Debug.Log($"move input is {mMoveInput}");
    }
    public void PerformJump(InputAction.CallbackContext context) //performing a jump
    {
        Debug.Log($"Jumping");
        if(!mIsInAir) //checking if character is on ground and if so jump else dont allow another jump till on ground again 
        {
            mShouldJump = true;
        }
    }
    bool IsInAir() //more forgiving check to see if player is touching the ground **cushions the ground check or gives a looser check**
    {
        if(mCharacterController.isGrounded)
        {
            return false;
        }
        Collider[] airCheck = Physics.OverlapSphere(transform.position, mAirCheckRadius, mAirCheckLayerMask);
        foreach (Collider col in airCheck)
        {
            if(col.gameObject != gameObject)
            {
                return false;
            }
        }
        return true;
    }
    void Update()
    {
        mIsInAir = IsInAir();

        UpdateAnimation();
        UpdatingVerticalVelocity();
        UpdateHorizontalVelocity();
        UpdateTransform();
    }
    #region Speed
    private void UpdateAnimation()
    {
        mAnimator.SetFloat("Speed", mHorizontalVelocity.magnitude);
        mAnimator.SetBool("Landed", !mIsInAir);
    }
    private void UpdateTransform()
    {
        mCharacterController.Move((mHorizontalVelocity + mVerticalVelocity) * Time.deltaTime); //actually making the player move
        if (mHorizontalVelocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(mHorizontalVelocity.normalized, Vector3.up), Time.deltaTime * mTurnLerpRate);
        }
    }
    private void UpdatingVerticalVelocity()
    {
        if(mShouldJump && !mIsInAir) //trying jump 
        {
            mVerticalVelocity.y = mJumpSpeed;
            mShouldJump = false;
            mAnimator.SetTrigger("Jump");
            return;
        }
        if(mCharacterController.isGrounded) //we are on the ground, set the velocity to a small velocity
        {
            mAnimator.ResetTrigger("Jump");
            mVerticalVelocity.y = -1f;
            return;
        }
        if (mVerticalVelocity.y > -mMaxFallSpeed) //freefalling
        {
            mVerticalVelocity.y += Physics.gravity.y * Time.deltaTime; //making the player fall by the force of gravity 
        }
    }
    private void UpdateHorizontalVelocity()
    {
        Vector3 moveDir = PlayerInputToWoldDir(mMoveInput); //movement along the y axis
        float acceleration = mCharacterController.isGrounded ? mGroundMoveSpeedAcceleration : mAirMoveSpeedAcceleration; //checking if grounded or in air then using the proper acceleration
        if(moveDir.sqrMagnitude > 0)
        {
            mHorizontalVelocity += moveDir * acceleration * Time.deltaTime; //movement on the x axis
            mHorizontalVelocity = Vector3.ClampMagnitude(mHorizontalVelocity, mMaxMoveSpeed); //limiting speed
        }
        else
        {
            if(mHorizontalVelocity.sqrMagnitude > 0)
            {
                mHorizontalVelocity -= mHorizontalVelocity.normalized * acceleration * Time.deltaTime; //could make a variable to control deceleration to be more flexable 
                if(mHorizontalVelocity.sqrMagnitude < 0.1)
                {
                     mHorizontalVelocity = Vector3.zero;
                }
            }
        }
    }
    Vector3 PlayerInputToWoldDir(Vector2 inputVal) //calculates the look direction of the camera and player
    {
        Vector3 rightDir = Camera.main.transform.right;
        Vector3 forwardDir = Vector3.Cross(rightDir, Vector3.up); //calculates forward even when the camera is tilted 

        return rightDir * inputVal.x + forwardDir * inputVal.y;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = mIsInAir ? Color.red : Color.green;
        Gizmos.DrawSphere(transform.position, mAirCheckRadius);
    }
    #endregion
}
