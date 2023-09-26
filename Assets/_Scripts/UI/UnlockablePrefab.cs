using System;
using UnityEngine;

public abstract class UnlockablePrefab : MonoBehaviour
{
    public static event Action ElementPurchased;

    protected void RaisePurchaseEvent()
    {
        ElementPurchased?.Invoke();
    }
}
