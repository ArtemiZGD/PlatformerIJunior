using UnityEngine;

public class PlayerAimation : MonoBehaviour
{
    private readonly int Running = Animator.StringToHash(nameof(Running));
    private readonly int Grounded = Animator.StringToHash(nameof(Grounded));
    private readonly int Climbing = Animator.StringToHash(nameof(Climbing));
    private readonly int MoveUp = Animator.StringToHash(nameof(MoveUp));

    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _playerMovement;

    private Vector3 _initialScale;

    private void Awake()
    {
        _initialScale =  _animator.transform.localScale;
    }

    private void Update()
    {
        Vector2 velocity = _playerMovement.Rigidbody.velocity;

        _animator.SetBool(Grounded, _playerMovement.IsGrounded);
        _animator.SetBool(Climbing, _playerMovement.IsOnLadder);
        _animator.SetBool(Running, velocity.x != 0);
        _animator.SetBool(MoveUp, velocity.y > 0);

        _animator.transform.localScale = new Vector3(
            Mathf.Sign(velocity.x) * _initialScale.x, 
            _initialScale.y, 
            _initialScale.z);

        _animator.speed = _playerMovement.IsOnLadder && (_playerMovement.IsGrounded == false) ? 
            Mathf.Abs(velocity.y / _playerMovement.MaxMoveSpeed) : 
            Mathf.Abs(velocity.x / _playerMovement.MaxMoveSpeed);
    }
}
