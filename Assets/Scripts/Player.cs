using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Flipper))]
[RequireComponent(typeof(Jumper))]

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
        _currentRotationY = transform.rotation.y;

        _playerControls = new InputSystemActions();

        _mover.Initialize(_moveSpeed, _rigidBody);

        _groundChecker.GroundedStateChanged += ChangeGroundedState;
        _wallChecker.WallNearbyStatusChanged += ChangeWallFacingState;
    }

    private void FixedUpdate()
    {
        _currentRotationY = _flipper.HandleFacingDirection(_moveDirectionHorizontal, _currentRotationY, _isDefaultFacingRight);

        _mover.HandleMovement(_moveDirectionHorizontal, _isFacingWall);

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
                PlayAnimation("Wall-Slide");
                return;
            }

            if (_rigidBody.linearVelocityY > 0)
            {
                PlayAnimation("Jump");
            }
            else if (_rigidBody.linearVelocityY >= _minJumpToFallVelocityY && _rigidBody.linearVelocityY <= _maxJumpToFallVelocityY)
            {
                PlayAnimation("JumptoFall");
            }
            else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("JumptoFall") == false)
            {
                PlayAnimation("Fall");
            }
        }
        else
        {
            if (_mover.CurrentHorizontalSpeed != 0)
            {
                PlayAnimation("Run");
            }
            else
            {
                PlayAnimation("Idle");
            }
        }
    }
}
