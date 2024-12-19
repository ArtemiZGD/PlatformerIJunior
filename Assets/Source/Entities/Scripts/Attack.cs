using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    private const int One = 1;

    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed;

    private float _attackTimer = 0;

    protected int Damage => _damage;

    private void Update()
    {
        ProcessReload();
    }

    private void LateUpdate()
    {
        ProcessAttack();
    }

    protected abstract void ProcessAttack();

    protected bool IsAbleToAttack()
    {
        if (_attackTimer >= One / _attackSpeed) return true;

        return false;
    }

    protected void ResetTimer()
    {
        _attackTimer = 0;
    }

    private void ProcessReload()
    {
        _attackTimer += Time.deltaTime;
    }
}
