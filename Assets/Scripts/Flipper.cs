using UnityEngine;

public class Flipper : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private float _currentRotationY;
    private float _faceRightRotationValueY;
    private float _faceLeftRotationValueY;


    public void HandleFacingDirection(float moveDirectionHorizontal)
    {
        if (moveDirectionHorizontal != 0)
        {
            if (moveDirectionHorizontal == Vector2.right.x && _currentRotationY != _faceRightRotationValueY)
            {
                _currentRotationY = _faceRightRotationValueY;
            }
            else if (moveDirectionHorizontal == Vector2.left.x && _currentRotationY != _faceLeftRotationValueY)
            {
                _currentRotationY = _faceLeftRotationValueY;
            }

            transform.rotation = Quaternion.Euler(0, _currentRotationY, 0);
            _currentRotationY = _rigidBody.transform.rotation.y;
        }
    }

    public void Initialize(float moveSpeed, Rigidbody2D rigidBody, bool isDefaultFacingRight)
    {
        _rigidBody = rigidBody;
        _currentRotationY = _rigidBody.transform.rotation.y;

        if (isDefaultFacingRight == true)
        {
            _faceRightRotationValueY = 0f;
            _faceLeftRotationValueY = 180f;
        }
        else
        {
            _faceRightRotationValueY = 180f;
            _faceLeftRotationValueY = 0;
        }
    }
}
