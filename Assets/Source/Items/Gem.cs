using UnityEngine;

public class Gem : MonoBehaviour, Item
{
    private ItemCollector _itemCollector;

    private void Awake()
    {
        _itemCollector = FindFirstObjectByType<ItemCollector>();
    }

    public void Collect()
    {
        _itemCollector.AddGem();
        Destroy(gameObject);
    }
}
