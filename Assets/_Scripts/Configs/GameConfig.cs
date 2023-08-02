using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "_drinkData/GameConfig")]
public class GameConfig : ScriptableObject
{
    public int GameDuration;
    public float DrinkingDuration;
    [Header("����� ��������� ������� �������")]
    public int UncommonDrinkAppearanceChance;
    public int RareDrinkAppearanceChance;
    [Header("�������� ��������")]
    public float NormalSpeed;
    public float FastSpeed;
    public float SuperFastSpeed;

}
