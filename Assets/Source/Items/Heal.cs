using UnityEngine;

public class Heal : MonoBehaviour, Item
{
    [SerializeField] private int _healAmount;

    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    public void Collect()
    {
        _playerHealth.Heal(_healAmount);
        Destroy(gameObject);
    }
}
