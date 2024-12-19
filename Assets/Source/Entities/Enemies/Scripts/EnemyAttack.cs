using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed;

    private float _attackTimer = 0;
    private PlayerHealth _playerHealth = null;

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
        other.TryGetComponent(out _playerHealth);
    }

    private void OnTriggerExit2D(Collider2D other)
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

    private void ProcessReload()
    {
        _attackTimer += Time.deltaTime;
    }

    private void ProcessAttack()
    {
        if (IsAbleToAttack() && _playerHealth != null)
        {
            _attackTimer = 0;
            _playerHealth.TakeDamage(_damage);
        }
    }

    private bool IsAbleToAttack()
    {
        if (_attackTimer >= 1 / _attackSpeed) return true;

        return false;
    }
}
