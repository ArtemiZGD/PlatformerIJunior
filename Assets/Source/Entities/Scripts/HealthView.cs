using UnityEngine;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private SmoothBar _bar;

    private void Start()
    {
        _bar.Initialize(_health.CurrentHealth / _health.MaxHealth);
    }

    private void OnEnable()
    {
        _health.ChangeHealth += OnChangeHealth;
    }

    private void OnDisable()
    {
        _health.ChangeHealth -= OnChangeHealth;
    }

    private void OnChangeHealth(float health)
    {
        _bar.UpdateTargetAmount(health);
    }
}
