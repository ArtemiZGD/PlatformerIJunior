using UnityEngine;

public class HotKeys : MonoBehaviour
{
    [SerializeField] private KeyCode _vampirismKey;

    public KeyCode VampirismKey => _vampirismKey;
}
