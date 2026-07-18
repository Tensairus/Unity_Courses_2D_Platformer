using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Flipper))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(CoinPicker))]

public class Player : Character
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundingForce;
    [SerializeField] private Mover _mover;
    [SerializeField] private Flipper _flipper;
    [SerializeField] private Jumper _jumper;

    private InputSystemActions _playerControls;

    private float _minJumpToFallVelocityY = -0.1f;
    private float _maxJumpToFallVelocityY = 0.1f;

    private void Awake()
    {
        _playerControls = new InputSystemActions();

        _mover.Initialize(_moveSpeed, _rigidBody);
        _flipper.Initialize(_moveSpeed, _rigidBody, _isSpriteDefaultFacingRight);

        _groundChecker.GroundedStateChanged += ChangeGroundedState;
        _wallChecker.WallNearbyStatusChanged += ChangeWallFacingState;
    }

    private void FixedUpdate()
    {
        _mover.HandleMovement(_moveDirectionHorizontal, _isFacingWall);
        _flipper.HandleFacingDirection(_moveDirectionHorizontal);

        HandleAnimation();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    public void ProcessMovementInput(InputAction.CallbackContext context)
    {
        _moveDirectionHorizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        _jumper.Jump(_isGrounded, _jumpForce, _rigidBody);
    }

    private void ChangeGroundedState(bool newState)
    {
        _isGrounded = newState;
    }

    private void ChangeWallFacingState(bool newState)
    {
        _isFacingWall = newState;
    }

    private void HandleAnimation()
    {
        if (!_isGrounded)
        {
            if (_isFacingWall && _rigidBody.linearVelocityY < 0)
            {
                PlayAnimation(PlayerAnimatorData.Animations.WallSlideHash);
            }
            else if (_rigidBody.linearVelocityY >= _minJumpToFallVelocityY && _rigidBody.linearVelocityY <= _maxJumpToFallVelocityY)
            {
                PlayAnimation(PlayerAnimatorData.Animations.JumpToFallHash);
            }
            else if (_animator.GetCurrentAnimatorStateInfo(0).shortNameHash != PlayerAnimatorData.Animations.JumpToFallHash && _rigidBody.linearVelocityY < 0)
            {
                PlayAnimation(PlayerAnimatorData.Animations.FallHash);
            }
            else if (_rigidBody.linearVelocityY > 0)
            {
                PlayAnimation(PlayerAnimatorData.Animations.JumpHash);
            }
        }
        else if (Mathf.Abs(_mover.CurrentHorizontalSpeed) >= 0.001f)
        {
            PlayAnimation(PlayerAnimatorData.Animations.RunHash);
        }
        else
        {
            PlayAnimation(PlayerAnimatorData.Animations.IdleHash);
        }
    }
}
