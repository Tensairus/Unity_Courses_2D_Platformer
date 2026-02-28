using UnityEngine;

public class BasicEnemy : Character
{
    private void Awake()
    {
        _groundChecker.GroundedStateChanged += EvadePitFall;
        _wallChecker.WallNearbyStatusChanged += EvadeWall;
    }

    private void Update()
    {
        HandleMovement();
        HandleAnimation();
    }

    private void HandleMovement()
    {
        if (_isGrounded == true && _isFacingWall == false)
        {
            _rigidBody.linearVelocityX = _moveDirectionHorizontal * _moveSpeed;
        }
    }

    private void HandleAnimation()
    {
        if (_moveDirectionHorizontal != 0)
        {
            PlayAnimation("Walk");
        }
        else
        {
            PlayAnimation("Idle");
        }
    }

    private void EvadePitFall(bool isGroundAvailable)
    {
        _isGrounded = isGroundAvailable;

        if (_isGrounded == false)
        {
            TurnAround();
            ChangeHorizontalMovementDirection();
        }
    }

    private void EvadeWall(bool isFacingWall)
    {
        _isFacingWall = isFacingWall;

        if (_isFacingWall == true)
        {
            TurnAround();
            ChangeHorizontalMovementDirection();
        }
    }

    private void TurnAround()
    {
        int rotationValueY = 0;

        if (_moveDirectionHorizontal < 0)
        {
            rotationValueY = 180;
        }

        transform.rotation = Quaternion.Euler(0, rotationValueY, 0);
    }

    private void ChangeHorizontalMovementDirection()
    {
        if (_moveDirectionHorizontal == Vector2.right.x)
        {
            _moveDirectionHorizontal = Vector2.left.x;
        }
        else
        {
            _moveDirectionHorizontal = Vector2.right.x;
        }
    }
}