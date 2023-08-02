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

}
