using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class YandexSaveSystem : ISaveSystem
{
    public SaveData Load()
    {
        return new SaveData(YandexGame.savesData);
    }

    public void Save(SaveData data)
    {
        
    }
}
