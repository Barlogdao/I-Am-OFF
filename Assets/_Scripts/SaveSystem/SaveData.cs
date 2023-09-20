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
    public List<int> UnlockedBackgrounds = new() { 0 };
    public int CurrentBackGroundID = 0;

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
        UnlockedRecipes = data.UnlockedRecipes;
        UnlockedBackgrounds = data.UnlockedBackgrounds;
        CurrentBackGroundID = data.CurrentBackgroundID;
    }

    public bool RecipeIsUnlocked(CoctailRecipeSO recipe)
    {
        return UnlockedRecipes.Contains(recipe.Name);
    }

    public bool IsBackroundUnlocked(int backgroundID)
    {
        return UnlockedBackgrounds.Contains(backgroundID);
    }

    public void UnlockRecipe(CoctailRecipeSO recipe)
    {
        UnlockedRecipes += $",{recipe.Name}";
    }

    public void UnlockBackground(int backgroundID)
    {
        UnlockedBackgrounds.Add(backgroundID);
    }
}
