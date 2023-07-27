
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

        public bool AlkodzillaUnlocked = false;
        public bool SoberManUnlocked = false;
        public int PlayerCoins = 0;
        public int MaxScore = 0;
        public string UnlockedRecipes = "Mega Beer";

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны
        // Пока выявленное ограничение - это расширение массива


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            

            // Длина массива в проекте должна быть задана один раз!
            // Если после публикации игры изменить длину массива, то после обновления игры у пользователей сохранения могут поломаться
            // Если всё же необходимо увеличить длину массива, сдвиньте данное поле массива в самую нижнюю строку кода
        }
        public SavesYG(SaveData data)
        {
            AlkodzillaUnlocked = data.AlkodzillaUnlocked;
            SoberManUnlocked= data.SoberManUnlocked;
            PlayerCoins = data.PlayerCoins;
            MaxScore = data.MaxScore;
            UnlockedRecipes= data.UnlockedRecipes;
        }
    }
}
