using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health;

    public Action<float> ChangeHealth;

    private float _maxHealth;

    public float CurrentHealth => _health;
    public float MaxHealth => _maxHealth;

    private void Awake()
    {
        _maxHealth = _health;
    }

    public void Heal(float healAmount)
    {
        _health += Mathf.Max(0, healAmount);
        _health = Mathf.Min(_health, _maxHealth);
        ChangeHealth?.Invoke(_health / _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        _health -= Mathf.Max(0, damage);

        if (_health <= 0)
        {
            _health = 0;
            OnDeath();
        }

        ChangeHealth?.Invoke(_health / _maxHealth);
    }

    protected virtual void OnDeath() { }
}
