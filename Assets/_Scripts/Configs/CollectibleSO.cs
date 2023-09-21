using UnityEngine;

public class CollectibleSO : ScriptableObject
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public EarnType EarnType { get; private set; }
}
public enum EarnType
{
    Basic,
    Purchase,
    Reward,
    Trigger
}