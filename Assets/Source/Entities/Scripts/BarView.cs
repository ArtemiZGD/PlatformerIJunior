using System;
using UnityEngine;

public abstract class BarView : MonoBehaviour
{
    public Action<float> BarAmountChanged { get; set; }

    public abstract float GetStartValue();
}
