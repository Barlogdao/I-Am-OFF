
using YG;

public class YandexSaveSystem : ISaveSystem
{
    public SaveData Load()
    {
        return new SaveData(YandexGame.savesData);
    }

    public void Save(SaveData data)
    {
        SavesYG savedata = YandexGame.savesData;

        savedata.AlkodzillaUnlocked = data.AlkodzillaUnlocked;
        savedata.SoberManUnlocked = data.SoberManUnlocked;
        savedata.PlayerCoins = data.PlayerCoins;
        savedata.MaxScore = data.MaxScore;
        savedata.UnlockedRecipes = data.UnlockedRecipes;
        YandexGame.SaveProgress();
    }
}
