using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BatMovement : MonoBehaviour
{
    [SerializeField] private float _distanceToTarget;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private BatSpawnPoints _batSpawnPoints;

    private Transform _player;
    private Transform _target;
    private NavMeshAgent _navMeshAgent;
    private bool _isFollowingPlayer;

    public Vector3 Velocity => _navMeshAgent.velocity;
    public bool IsFollowingPlayer => _isFollowingPlayer;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _isFollowingPlayer = true;
        _player = _playerMovement.transform; 
        _target = _player;
    }

    private void Update()
    {
        SetTarget();
    }

    private void SetTarget()
    {
        if (_player == null || transform.position.IsEnoughClose(_player.position, _distanceToTarget) == false)
        {
            if (_isFollowingPlayer)
            {
                _target = _batSpawnPoints.FindEmptyTarget(_navMeshAgent);
                _isFollowingPlayer = false;
            }
        }
        else
        {
            if (_isFollowingPlayer == false)
            {
                _batSpawnPoints.FreeTarget(_target);
                _target = _player;
                _isFollowingPlayer = true;
            }
        }

        _navMeshAgent.SetDestination(_target.position);
    }
}
