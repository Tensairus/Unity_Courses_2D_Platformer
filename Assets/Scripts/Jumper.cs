using UnityEngine;

public class Jumper : MonoBehaviour
{
    public void Jump(bool isGrounded, float jumpForce, Rigidbody2D rigidBody)
    {
        if (isGrounded == true)
        {
            rigidBody.linearVelocityY = jumpForce;
        }
    }
}
