using System.Collections.Generic;
using YG;

[System.Serializable]
public class SaveData
{
    public bool AlkodzillaUnlocked;
    public bool SoberManUnlocked;
    public int PlayerCoins;
    public int MaxScore;

    
    public string UnlockedRecipes = "Mega Beer";

    public SaveData()
    {
        UnlockedRecipes = "Mega Beer";
        SoberManUnlocked = false;
        AlkodzillaUnlocked = false;
        PlayerCoins = 0;
        MaxScore = 0;
    }

    public SaveData(SavesYG data)
    {
        AlkodzillaUnlocked = data.AlkodzillaUnlocked;
        SoberManUnlocked= data.SoberManUnlocked;
        PlayerCoins = data.PlayerCoins;
        MaxScore = data.MaxScore;
        UnlockedRecipes += data.UnlockedRecipes;

    }

    public bool RecipeIsUnlocked(CoctailRecipeSO recipe)
    {
        return UnlockedRecipes.Contains(recipe.Name);
    }

    public void UnlockRecipe(CoctailRecipeSO recipe)
    {
        UnlockedRecipes += $",{recipe.Name}";
    }
}
