using System.Collections.Generic;
using YG;

[System.Serializable]
public class SaveData
{

    public int PlayerCoins;
    public int MaxScore;
    public List<int> UnlockedRecipes = new() { 0 };
    public List<int> UnlockedBackgrounds = new() { 0 };
    public List<int> UnlockedPlayers = new() { 0 };
    public int CurrentBackGroundID = 0;
    public int CurrentPlayerID = 0;

    public SaveData()
    {
        PlayerCoins = 0;
        MaxScore = 0;
    }

    public SaveData(SavesYG data)
    {
        PlayerCoins = data.PlayerCoins;
        MaxScore = data.MaxScore;
        UnlockedRecipes = data.UnlockedRecipes;
        UnlockedBackgrounds = data.UnlockedBackgrounds;
        UnlockedPlayers = data.UnlockedPlayers;
        CurrentBackGroundID = data.CurrentBackgroundID;
        CurrentPlayerID = data.CurrentPlayerID;
    }

    public bool IsRecipeUnlocked(int cocktailID)
    {
        return UnlockedRecipes.Contains(cocktailID);
    }

    public bool IsBackgroundUnlocked(int backgroundID)
    {
        return UnlockedBackgrounds.Contains(backgroundID);
    }
    public bool IsPlayerUnlocked(int playerID)
    {
        return UnlockedPlayers.Contains(playerID);
    }

    public void UnlockRecipe(int cocktailID)
    {
        UnlockedRecipes.Add(cocktailID);
    }

    public void UnlockBackground(int backgroundID)
    {
        UnlockedBackgrounds.Add(backgroundID);
    }

    public void UnlockPlayer(int playerID)
    {
        UnlockedPlayers.Add(playerID);
    }

}
