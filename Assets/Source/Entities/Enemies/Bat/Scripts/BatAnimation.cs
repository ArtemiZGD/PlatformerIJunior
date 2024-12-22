using UnityEngine;

public class BatAnimation : MonoBehaviour
{
    private const float MinVelocity = 0.1f;
    private const string IsMoving = nameof(IsMoving);

    [SerializeField] private Animator _animator;
    [SerializeField] private BatMovement _movement;

    private readonly int _isMoving = Animator.StringToHash(IsMoving);

    private Vector3 _initialScale;

    private void Awake()
    {
        _initialScale =  _animator.transform.localScale;
    }

    private void Update()
    {
        bool isMoving = _movement.Velocity.magnitude > MinVelocity || _movement.IsFollowingPlayer;
        _animator.SetBool(_isMoving, isMoving);
        _animator.transform.localScale = new Vector3(
            Mathf.Sign(_movement.Velocity.x) * _initialScale.x, 
            _initialScale.y, 
            _initialScale.z);
    }
}
