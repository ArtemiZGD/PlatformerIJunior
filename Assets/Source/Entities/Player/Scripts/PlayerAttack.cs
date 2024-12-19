using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionDetector))]
public class PlayerAttack : Attack
{
    private List<EnemyHealth> _enemies = new();
    private CollisionDetector _collisionDetector;

    private void Awake()
    {
        _collisionDetector = GetComponent<CollisionDetector>();
    }

    private void OnEnable()
    {
        _collisionDetector.OnTriggerEnter += AddEnemyToList;
        _collisionDetector.OnTriggerExit += RemoveEnemyFromList;
    }

    private void OnDisable()
    {
        _collisionDetector.OnTriggerEnter -= AddEnemyToList;
        _collisionDetector.OnTriggerExit -= RemoveEnemyFromList;
    }

    private void AddEnemyToList(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyHealth enemyHealth))
        {
            _enemies.Add(enemyHealth);
        }
    }

    private void RemoveEnemyFromList(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyHealth enemyHealth))
        {
            _enemies.Remove(enemyHealth);
        }
    }

    protected override void ProcessAttack()
    {
        if (IsAbleToAttack() && _enemies.Count > 0)
        {
            ResetTimer();
            _enemies[0].TakeDamage(Damage);
        }
    }
}
