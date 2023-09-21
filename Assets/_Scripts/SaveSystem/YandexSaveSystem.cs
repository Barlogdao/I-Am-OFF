
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

        saveData.AlkodzillaUnlocked = data.AlkodzillaUnlocked;
        saveData.SoberManUnlocked = data.SoberManUnlocked;
        saveData.PlayerCoins = data.PlayerCoins;
        saveData.MaxScore = data.MaxScore;
        saveData.UnlockedRecipes = data.UnlockedRecipes;
        saveData.UnlockedBackgrounds = data.UnlockedBackgrounds;
        saveData.CurrentBackgroundID = data.CurrentBackGroundID;
        YandexGame.SaveProgress();
    }

    public void Reset()
    {
        YandexGame.ResetSaveProgress();
    }
}
