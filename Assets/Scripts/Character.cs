using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected string _name;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected Rigidbody2D _rigidBody;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _currentHorizontalSpeed;
    [SerializeField] protected float _currentVerticalSpeed;
    [SerializeField] protected float _moveDirectionHorizontal;
    [SerializeField] protected GroundChecker _groundChecker;
    [SerializeField] protected WallChecker _wallChecker;
    [SerializeField] protected bool _isGrounded;
    [SerializeField] protected bool _isFacingWall;

    protected void PlayAnimation(string animationName)
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            _animator.Play(animationName);
        }
    }
}