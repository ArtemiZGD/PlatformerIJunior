using UnityEngine;

public class Heal : Item
{
    [SerializeField] private float _amount;

    public float Amount => _amount;
}
