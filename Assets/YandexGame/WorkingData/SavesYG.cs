
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Ваши сохранения

        public int PlayerCoins = 0;
        public int MaxScore = 0;
        public List<int> UnlockedRecipes = new List<int>() { 0 };
        public List<int> UnlockedBackgrounds = new List<int>() { 0 };
        public List<int> UnlockedPlayers = new List<int>() { 0 };
        public int CurrentBackgroundID = 0;
        public int CurrentPlayerID = 0;

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {

        }
    }
}
