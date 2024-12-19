using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed;

    private float _attackTimer = 0;
    private List<EnemyHealth> _enemies = new();

    private void Update()
    {
        ProcessReload();
    }

    private void LateUpdate()
    {
        ProcessAttack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyHealth enemyHealth))
        {
            _enemies.Add(enemyHealth);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyHealth enemyHealth))
        {
            _enemies.Remove(enemyHealth);
        }
    }

    private void ProcessReload()
    {
        _attackTimer += Time.deltaTime;
    }

    private void ProcessAttack()
    {
        if (IsAbleToAttack() && _enemies.Count > 0)
        {
            _attackTimer = 0;
            _enemies[0].TakeDamage(_damage);
        }
    }

    private bool IsAbleToAttack()
    {
        if (_attackTimer >= 1 / _attackSpeed) return true;

        return false;
    }
}
