using System;
using UnityEngine;

[RequireComponent(typeof(CollisionDetector))]
public class ItemCollector : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;

    public Action<int> CollectGem;

    private CollisionDetector _collisionDetector;
    private int _gems = 0;

    private void Awake()
    {
        _collisionDetector = GetComponent<CollisionDetector>();
    }

    private void OnEnable()
    {
        _collisionDetector.OnTriggerEnter += CollectItem;
    }

    private void OnDisable()
    {
        _collisionDetector.OnTriggerEnter -= CollectItem;
    }

    private void Start()
    {
        CollectGem?.Invoke(_gems);
    }

    private void CollectItem(Collider2D other)
    {
        if (other.TryGetComponent(out Item item))
        {
            if (item is Gem gem)
            {
                AddGem();
            }
            else if (item is Heal heal)
            {
                HealPlayer(heal.Amount);
            }
            
            item.Collect();
        }
    }

    private void AddGem()
    {
        CollectGem?.Invoke(++_gems);
    }

    private void HealPlayer(float amount)
    {
        _playerHealth.Heal(amount);
    }
}