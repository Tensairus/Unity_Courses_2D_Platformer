using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundingForce;
    [SerializeField] private float _currentRotationY;

    private InputSystemActions _playerControls;

    private float _minJumpToFallVelocityY = -0.1f;
    private float _maxJumpToFallVelocityY = 0.1f;

    private void Awake()
    {
        _currentRotationY = transform.rotation.y;

        _playerControls = new InputSystemActions();

        _groundChecker.GroundedStateChanged += ChangeGroundedState;
        _wallChecker.WallNearbyStatusChanged += ChangeWallFacingState;
    }

    private void FixedUpdate()
    {
        HandleMovement();
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

    public void Move(InputAction.CallbackContext context)
    {
        _moveDirectionHorizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (_isGrounded == true)
        {
            _rigidBody.linearVelocityY = _jumpForce;
        }
    }

    public void TurnAround()
    {
        float faceRightRotationValue = 0f;
        float faceLeftRotationValue = 180f;

        if (_moveDirectionHorizontal == Vector2.right.x && _currentRotationY != faceRightRotationValue)
        {
            _currentRotationY = faceRightRotationValue;
        }
        else if (_moveDirectionHorizontal == Vector2.left.x && _currentRotationY != faceLeftRotationValue)
        {
            _currentRotationY = faceLeftRotationValue;
        }

        transform.rotation = Quaternion.Euler(0, _currentRotationY, 0);
    }

    private void ChangeGroundedState(bool newState)
    {
        _isGrounded = newState;
    }

    private void ChangeWallFacingState(bool newState)
    {
        _isFacingWall = newState;
    }

    private void HandleMovement()
    {
        if (_moveDirectionHorizontal != 0 && _isFacingWall == false)
        {
            _currentHorizontalSpeed = _moveDirectionHorizontal * _moveSpeed;
        }
        else
        {
            _currentHorizontalSpeed = 0f;
        }

        _rigidBody.linearVelocityX = _currentHorizontalSpeed;
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
            if (_currentHorizontalSpeed != 0)
                PlayAnimation("Run");
            else
                PlayAnimation("Idle");
        }
    }
}
