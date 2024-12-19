using UnityEngine;

public class BatAnimation : MonoBehaviour
{
    private readonly int IsMoving = Animator.StringToHash(nameof(IsMoving));

    [SerializeField] private Animator _animator;
    [SerializeField] private BatMovement _batMovement;

    private Vector3 _initialScale;

    private void Awake()
    {
        _initialScale =  _animator.transform.localScale;
    }

    private void Update()
    {
        bool isMoving = _batMovement.Velocity.magnitude > 0.1f || _batMovement.IsFollowingPlayer;
        _animator.SetBool(IsMoving, isMoving);
        _animator.transform.localScale = new Vector3(
            Mathf.Sign(_batMovement.Velocity.x) * _initialScale.x, 
            _initialScale.y, 
            _initialScale.z);
    }
}
