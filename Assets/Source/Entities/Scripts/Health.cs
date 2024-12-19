using System;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private float _health;

    public Action<float> HealthChanged;

    private float _maxHealth;

    public float MaxHealth => _maxHealth;

    private void Awake()
    {
        _maxHealth = _health;
    }

    public void Heal(float healAmount)
    {
        _health += Mathf.Max(0, healAmount);
        _health = Mathf.Min(_health, _maxHealth);
        HealthChanged?.Invoke(_health);
    }

    public void TakeDamage(float damage)
    {
        _health -= Mathf.Max(0, damage);

        if (_health <= 0)
        {
            _health = 0;
            OnDeath();
        }

        HealthChanged?.Invoke(_health);
    }

    protected abstract void OnDeath();
}
