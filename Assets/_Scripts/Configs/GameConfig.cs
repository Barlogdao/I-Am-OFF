using UnityEngine;


[CreateAssetMenu(fileName = "GameConfig", menuName = "_drinkData/GameConfig")]
public class GameConfig : ScriptableObject
{
    public int GameDuration;
    public float DrinkingDuration;
    [Header("Шансы появления редкого напитка")]
    public int UncommonDrinkAppearanceChance;
    public int RareDrinkAppearanceChance;

    [Header("Скорость напитков")]
    public float NormalSpeed;
    public float FastSpeed;
    public float SuperFastSpeed;

    [Header ("Пределы Характеристик игрока")]
    [field: SerializeField] public int MinStamina;
    [field: SerializeField] public int MaxStamina;
    [field: SerializeField] public int MinDrunkModifier;
    [field: SerializeField] public int MaxDrunkModifier;
    [field: SerializeField] public float MinHangoverTime;
    [field: SerializeField] public float MaxHangoverTime;


    private void OnValidate()
    {
        if (MinStamina >= MaxStamina)
            MaxStamina = MinStamina + 1;
        if (MinDrunkModifier >= MaxDrunkModifier)
            MaxDrunkModifier = MinDrunkModifier + 1;
        if (MinHangoverTime >= MaxHangoverTime)
            MaxHangoverTime = MinHangoverTime + 1;
    }
}
