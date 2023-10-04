
using UnityEngine;

[CreateAssetMenu(fileName = "NewDrinkSO", menuName = "_drinkData/Drinks")]
public class DrinkSO : ScriptableObject
{
    public string Name;
    public Sprite Image;
    public int BonusStrength = 0;

    public DrinkRarity Rarity;
    public DrinkStrength Strength;
    public int GetDrinkStrengthLevel()
    {
        int strength = 0;
        switch (Strength)
        {
            case DrinkStrength.NonAlcohol:
                strength = 2;
                break;
            case DrinkStrength.Light:
                strength = 4;
                break;
            case DrinkStrength.Medium:
                strength = 7;
                break;
            case DrinkStrength.Hard:
                strength = 10;
                break;
        }
        return strength + BonusStrength;
    }
    public int GetDrinkScore()
    {
        int score = 0;
        switch (Rarity)
        {
            case DrinkRarity.Common:
                score = 15;
                break;
            case DrinkRarity.Uncommon:
                score = 35;
                break;
            case DrinkRarity.Rare:
                score = 50;
                break;
        }
        return score;
    }

}


public enum DrinkRarity
{
    Common,
    Uncommon,
    Rare
}

public enum DrinkStrength
{
    NonAlcohol,
    Light,
    Medium,
    Hard
}


