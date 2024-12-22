using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerAimation : MonoBehaviour
{
    private const string Running = nameof(Running);
    private const string Grounded = nameof(Grounded);
    private const string Climbing = nameof(Climbing);
    private const string MoveUp = nameof(MoveUp);

    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _movement;

    private readonly int _running = Animator.StringToHash(Running);
    private readonly int _grounded = Animator.StringToHash(Grounded);
    private readonly int _climbing = Animator.StringToHash(Climbing);
    private readonly int _moveUp = Animator.StringToHash(MoveUp);

    private Vector3 _initialScale;

    private void Awake()
    {
        _initialScale =  _animator.transform.localScale;
    }

    private void Update()
    {
        Vector2 velocity = _movement.Rigidbody.velocity;

        _animator.SetBool(_grounded, _movement.IsGrounded);
        _animator.SetBool(_climbing, _movement.IsOnLadder);
        _animator.SetBool(_running, velocity.x != 0);
        _animator.SetBool(_moveUp, velocity.y > 0);

        _animator.transform.localScale = new Vector3(
            Mathf.Sign(velocity.x) * _initialScale.x, 
            _initialScale.y, 
            _initialScale.z);

        _animator.speed = _movement.IsOnLadder && (_movement.IsGrounded == false) ? 
            Mathf.Abs(velocity.y / _movement.MaxMoveSpeed) : 
            Mathf.Abs(velocity.x / _movement.MaxMoveSpeed);
    }
}
