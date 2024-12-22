using TMPro;
using UnityEngine;

public class GemsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _gemsText;
    [SerializeField] private ItemCollector _itemCollector;

    private void OnEnable()
    {
        _itemCollector.CollectGem += Display;
    }

    private void OnDisable()
    {
        _itemCollector.CollectGem -= Display;
    }

    private void Display(int gems)
    {
        _gemsText.text = $"Gems: {gems}";
    }
}
