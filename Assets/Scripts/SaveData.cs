using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public bool AlkodzillaUnlocked;
    public bool SoberManUnlocked;
    public int PlayerCoins;
    public int MaxScore;

    
    public string UnlockedRecipies = "Mega Beer";

    public SaveData()
    {
        UnlockedRecipies = "Mega Beer";
        SoberManUnlocked = false;
        AlkodzillaUnlocked = false;
        PlayerCoins = 0;
        MaxScore = 0;
    }

    public bool RecipeIsUnlocked(CoctailRecipeSO recipe)
    {
        return UnlockedRecipies.Contains(recipe.Name);
    }

    public void UnlockRecipe(CoctailRecipeSO recipe)
    {
        UnlockedRecipies += $",{recipe.Name}";
    }
}
