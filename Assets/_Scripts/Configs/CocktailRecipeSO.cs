using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCocktail", menuName = "_drinkData/Cocktail")]
public class CocktailRecipeSO : CollectibleSO
{
    public Sprite Image;
    public string Name;
    public List<DrinkSO> Recipe;
    public int BonusScore;
    public int BonusTime;
    public int CoinCost = 1;

    public bool StomachHasRecipe(List<DrinkSO> stomach)
    {
        if (Recipe.Count != stomach.Count)
        {
            return false;
        }

        for (int i = 0; i < Recipe.Count; i++)
        {
            if (Recipe[i] != stomach[i])
            {
                return false;
            }
        }
        return true;
    }

}