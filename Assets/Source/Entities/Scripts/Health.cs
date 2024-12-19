using UnityEngine;

public class Health : BarView
{
    [SerializeField] private float _health;

    private float _maxHealth;

    private void Awake()
    {
        _maxHealth = _health;
    }

    public override float GetStartValue()
    {
        return 1;
    }

    public void Heal(float healAmount)
    {
        _health += Mathf.Max(0, healAmount);
        _health = Mathf.Min(_health, _maxHealth);
        BarAmountChanged?.Invoke(_health / _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        _health -= Mathf.Max(0, damage);

        if (_health <= 0)
        {
            _health = 0;
            OnDeath();
        }

        BarAmountChanged?.Invoke(_health / _maxHealth);
    }

    protected virtual void OnDeath() { }
}
