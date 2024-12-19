using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public virtual void Collect()
    {
        Destroy(gameObject);
    }
}
