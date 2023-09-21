using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundSO", menuName = "_drinkData/BackGrounds")]

public class BackgroundSO : CollectibleSO
{
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField,Min(0)] public int CoinCost { get; private set; }
}

