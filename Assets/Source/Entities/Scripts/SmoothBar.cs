using UnityEngine;
using UnityEngine.UI;

public class SmoothBar : MonoBehaviour
{
    [SerializeField] private Slider _bar;
    [SerializeField] private float _smoothSpeed = 5f;

    private float _currentAmount;

    private void Update()
    {
        UpdateBar();
    }

    public void Initialize(float startValue)
    {
        _bar.value = startValue;
        _currentAmount = startValue;
    }

    public void UpdateTargetAmount(float health)
    {
        _currentAmount = health;
    }

    private void UpdateBar()
    {
        _bar.value = Mathf.MoveTowards(_bar.value, _currentAmount, _smoothSpeed * Time.deltaTime);
    }
}
