using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health;

    public Action<int> HealthChanged;

    private int _maxHealth;

    public int MaxHealth => _maxHealth;

    private void Awake()
    {
        _maxHealth = _health;
    }

    public void Heal(int healAmount)
    {
        _health += Mathf.Max(0, healAmount);
        _health = Mathf.Min(_health, _maxHealth);
        HealthChanged?.Invoke(_health);
    }

    public void TakeDamage(int damage)
    {
        _health -= Mathf.Max(0, damage);

        if (_health <= 0)
        {
            _health = 0;
            OnDeath();
        }

        HealthChanged?.Invoke(_health);
    }

    virtual protected void OnDeath()
    {

    }
}
