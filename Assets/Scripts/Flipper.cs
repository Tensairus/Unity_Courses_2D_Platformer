using UnityEngine;

public class Flipper : MonoBehaviour
{


    public float HandleFacingDirection(float moveDirectionHorizontal, float currentRotationY, bool isDefaultFacingRight)
    {
        float faceRightRotationValueY;
        float faceLeftRotationValueY;

        if (isDefaultFacingRight == true)
        {
            faceRightRotationValueY = 0f;
            faceLeftRotationValueY = 180f;
        }
        else
        {
            faceRightRotationValueY = 180f;
            faceLeftRotationValueY = 0;
        }

        if (moveDirectionHorizontal == Vector2.right.x && currentRotationY != faceRightRotationValueY)
        {
            currentRotationY = faceRightRotationValueY;
        }
        else if (moveDirectionHorizontal == Vector2.left.x && currentRotationY != faceLeftRotationValueY)
        {
            currentRotationY = faceLeftRotationValueY;
        }

        transform.rotation = Quaternion.Euler(0, currentRotationY, 0);

        return currentRotationY;
    }
}
