using UnityEngine;
using UnityEngine.UI;

public class SmoothBar : MonoBehaviour
{
    [SerializeField] private BarView _barView;
    [SerializeField] private Slider _bar;
    [SerializeField] private float _smoothSpeed = 5f;

    private float _currentAmount;

    private void OnEnable()
    {
        _barView.BarAmountChanged += UpdateBar;
    }

    private void OnDisable()
    {
        _barView.BarAmountChanged -= UpdateBar;
    }

    private void Start()
    {
        float startValue = _barView.GetStartValue();
        _bar.value = startValue;
        _currentAmount = startValue;
    }

    private void Update()
    {
        UpdateBar();
    }

    private void UpdateBar(float health)
    {
        _currentAmount = health;
    }

    private void UpdateBar()
    {
        _bar.value = Mathf.MoveTowards(_bar.value, _currentAmount, _smoothSpeed * Time.deltaTime);
    }
}
