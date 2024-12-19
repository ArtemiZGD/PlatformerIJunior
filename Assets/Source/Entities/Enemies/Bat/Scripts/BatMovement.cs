using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BatMovement : MonoBehaviour
{
    [SerializeField] private float _distanceToTarget;

    private BatSpawnPoints _batSpawnPoints;
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
        _target = _player;
    }

    private void Start()
    {
        _player = FindFirstObjectByType<PlayerMovement>().transform;
        _batSpawnPoints = FindFirstObjectByType<BatSpawnPoints>();
    }

    private void Update()
    {
        SetTarget();
    }

    private void SetTarget()
    {
        if (_player == null || Vector3.Distance(transform.position, _player.position) > _distanceToTarget)
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
