
using YG;

public class YandexSaveSystem : ISaveSystem
{
    public SaveData Load()
    {
        return new SaveData(YandexGame.savesData);
    }

    public void Save(SaveData data)
    {
        SavesYG saveData = YandexGame.savesData;

        saveData.PlayerCoins = data.PlayerCoins;
        saveData.MaxScore = data.MaxScore;
        saveData.UnlockedRecipes = data.UnlockedRecipes;
        saveData.UnlockedBackgrounds = data.UnlockedBackgrounds;
        saveData.UnlockedPlayers = data.UnlockedPlayers;
        saveData.CurrentBackgroundID = data.CurrentBackGroundID;
        saveData.CurrentPlayerID = data.CurrentPlayerID;
        YandexGame.SaveProgress();
    }

    public void Reset()
    {
        YandexGame.ResetSaveProgress();
    }
}
