
using UnityEngine;

[CreateAssetMenu(fileName = "NewDrinkSO", menuName = "_drinkData/Drinks")]
public class DrinkSO : ScriptableObject
{
    public string Name;
    public Sprite Image;
    public int Strenght;
    public int Reward;
    public DrinkRarity Rarity;
    public DrinkType Type;
    

}


public enum DrinkRarity
{
    Common,
    Uncommon,
    Rare
}

public enum DrinkType
{
    Light,
    Medium,
    Hard
}
