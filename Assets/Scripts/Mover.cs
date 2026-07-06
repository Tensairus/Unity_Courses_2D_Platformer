using UnityEngine;

public class Mover : MonoBehaviour
{
    public float CurrentHorizontalSpeed => _currentHorizontalSpeed;

    private float _moveSpeed;
    private float _currentHorizontalSpeed;
    private Rigidbody2D _rigidBody;

    public void HandleMovement(float moveDirectionHorizontal, bool isFacingWall)
    {
        if (moveDirectionHorizontal != 0 && isFacingWall == false)
        {
            _currentHorizontalSpeed = moveDirectionHorizontal * _moveSpeed;
        }
        else
        {
            _currentHorizontalSpeed = 0f;
        }

        _rigidBody.linearVelocityX = _currentHorizontalSpeed;
    }

    public void Initialize(float moveSpeed, Rigidbody2D rigidBody)
    {
        _moveSpeed = moveSpeed;
        _rigidBody = rigidBody;
    }
}
