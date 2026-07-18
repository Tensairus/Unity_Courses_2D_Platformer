using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Rigidbody2D _rigidBody;
    [SerializeField] protected GroundChecker _groundChecker;
    [SerializeField] protected WallChecker _wallChecker;
    [SerializeField] protected string _name;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _moveDirectionHorizontal;
    [SerializeField] protected bool _isGrounded;
    [SerializeField] protected bool _isFacingWall;
    [SerializeField] protected bool _isSpriteDefaultFacingRight;

    [SerializeField] private int _currentAnimationHash;

    protected void PlayAnimation(int newAnimationHash)
    {
        if (_currentAnimationHash == newAnimationHash)
            return;

        _currentAnimationHash = newAnimationHash;
        _animator.Play(newAnimationHash);
    }
}