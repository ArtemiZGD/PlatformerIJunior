using UnityEngine;

[RequireComponent(typeof(CollisionDetector))]
public class EnemyAttack : Attack
{
    private PlayerHealth _playerHealth = null;
    private CollisionDetector _collisionDetector;

    private void Awake()
    {
        _collisionDetector = GetComponent<CollisionDetector>();
    }

    private void OnEnable()
    {
        _collisionDetector.OnTriggerEnter += ProcessPlayerEnter;
        _collisionDetector.OnTriggerExit += ProcessPlayerExit;
    }

    private void OnDisable()
    {
        _collisionDetector.OnTriggerEnter -= ProcessPlayerEnter;
        _collisionDetector.OnTriggerExit -= ProcessPlayerExit;
    }

    private void ProcessPlayerEnter(Collider2D other)
    {
        other.TryGetComponent(out _playerHealth);
    }

    private void ProcessPlayerExit(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerHealth _))
        {
            _playerHealth = null;
        }
    }

    private void OnDestroy()
    {
        ProcessAttack();
    }

    protected override void ProcessAttack()
    {
        if (IsAbleToAttack() && _playerHealth != null)
        {
            ResetTimer();
            _playerHealth.TakeDamage(Damage);
        }
    }
}
