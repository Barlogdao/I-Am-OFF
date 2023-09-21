using System.Collections.Generic;
using YG;

[System.Serializable]
public class SaveData
{
    public bool AlkodzillaUnlocked;
    public bool SoberManUnlocked;
    public int PlayerCoins;
    public int MaxScore;
    public List<int> UnlockedRecipes = new() { 0 };
    public List<int> UnlockedBackgrounds = new() { 0 };
    public int CurrentBackGroundID = 0;

    public SaveData()
    {
        SoberManUnlocked = false;
        AlkodzillaUnlocked = false;
        PlayerCoins = 0;
        MaxScore = 0;
    }

    public SaveData(SavesYG data)
    {
        AlkodzillaUnlocked = data.AlkodzillaUnlocked;
        SoberManUnlocked = data.SoberManUnlocked;
        PlayerCoins = data.PlayerCoins;
        MaxScore = data.MaxScore;
        UnlockedRecipes = data.UnlockedRecipes;
        UnlockedBackgrounds = data.UnlockedBackgrounds;
        CurrentBackGroundID = data.CurrentBackgroundID;
    }

    public bool IsRecipeUnlocked(int cocktailID)
    {
        return UnlockedRecipes.Contains(cocktailID);
    }

    public bool IsBackgroundUnlocked(int backgroundID)
    {
        return UnlockedBackgrounds.Contains(backgroundID);
    }

    public void UnlockRecipe(int cocktailID)
    {
        UnlockedRecipes.Add(cocktailID);
    }

    public void UnlockBackground(int backgroundID)
    {
        UnlockedBackgrounds.Add(backgroundID);
    }
}
