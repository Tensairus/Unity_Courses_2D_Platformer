using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Flipper))]

public class BasicEnemy : Character
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Flipper _flipper;

    private void Awake()
    {
        _mover.Initialize(_moveSpeed, _rigidBody);
        _flipper.Initialize(_moveSpeed, _rigidBody, _isSpriteDefaultFacingRight);

        _groundChecker.GroundedStateChanged += EvadePitFall;
        _wallChecker.WallNearbyStatusChanged += EvadeWall;
    }

    private void Update()
    {
        _mover.HandleMovement(_moveDirectionHorizontal, _isFacingWall);

        HandleAnimation();
    }

    private void HandleAnimation()
    {
        if (_moveDirectionHorizontal != 0 && _isGrounded == true)
        {
            PlayAnimation(EnemyAnimationData.BasicEnemy.Animations.WalkHash);
        }
        else
        {
            PlayAnimation(EnemyAnimationData.BasicEnemy.Animations.IdleHash);
        }
    }

    private void EvadePitFall(bool isGroundAvailable)
    {
        _isGrounded = isGroundAvailable;

        if (_isGrounded == false)
        {
            TurnAround();
        }
    }

    private void EvadeWall(bool isFacingWall)
    {
        _isFacingWall = isFacingWall;

        if (_isFacingWall == true)
        {
            TurnAround();
        }
    }

    private void TurnAround()
    {
        ChangeHorizontalMovementDirection();
        _flipper.HandleFacingDirection(_moveDirectionHorizontal);
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