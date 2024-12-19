using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private TMP_Text _gemsText;

    private int _gems = 0;

    private void Start()
    {
        UpdateGemsText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Item item))
        {
            item.Collect();
        }
    }

    public void AddGem()
    {
        _gems++;
        UpdateGemsText();
    }

    private void UpdateGemsText()
    {
        _gemsText.text = $"Gems: {_gems}";
    }
}