using UnityEngine;

public class PlayerPrefSaveSystem : ISaveSystem
{
    private const string SAVEDATA = "SAVE_DATA";
    public SaveData Load()
    {
        if (PlayerPrefs.HasKey(SAVEDATA))
        {
            return JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SAVEDATA));
        }
        else
        {
           return new SaveData();
        }
    }

    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVEDATA, json);
        PlayerPrefs.Save();
    }

    public void UpdateCloud()
    {
    }
}
