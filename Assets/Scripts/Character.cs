using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected Rigidbody2D _rigidBody;
    [SerializeField] protected GroundChecker _groundChecker;
    [SerializeField] protected WallChecker _wallChecker;
    [SerializeField] protected string _name;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _moveDirectionHorizontal;
    [SerializeField] protected float _currentVerticalSpeed;
    [SerializeField] protected float _currentRotationY;
    [SerializeField] protected bool _isGrounded;
    [SerializeField] protected bool _isFacingWall;
    [SerializeField] protected bool _isDefaultFacingRight;

    protected void PlayAnimation(string animationName)
    {
        int newAnimationNameHashed = Animator.StringToHash(animationName);

        if (_animator.GetCurrentAnimatorStateInfo(0).shortNameHash != newAnimationNameHashed)
        {
            _animator.Play(newAnimationNameHashed);
        }
    }
}