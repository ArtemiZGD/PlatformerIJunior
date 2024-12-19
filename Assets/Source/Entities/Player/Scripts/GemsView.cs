using TMPro;
using UnityEngine;

public class GemsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _gemsText;

    public void Display(int gems)
    {
        _gemsText.text = $"Gems: {gems}";
    }
}
