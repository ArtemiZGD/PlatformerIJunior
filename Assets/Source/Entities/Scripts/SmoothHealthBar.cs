using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _bar;
    [SerializeField] private float _smoothSpeed = 5f;

    private float _currentHealth;

    private void OnEnable()
    {
        _health.HealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= UpdateHealthBar;
    }

    private void Start()
    {
        _bar.value = _health.MaxHealth;
        _currentHealth = _health.MaxHealth;
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar(float health)
    {
        _currentHealth = health;
    }

    private void UpdateHealthBar()
    {
        _bar.value = Mathf.MoveTowards(_bar.value, _currentHealth / _health.MaxHealth, _smoothSpeed * Time.deltaTime);
    }
}
