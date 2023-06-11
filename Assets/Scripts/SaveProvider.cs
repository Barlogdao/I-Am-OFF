using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveProvider : MonoBehaviour
{
    public static SaveProvider Instace { get; private set; }
    public SaveData SaveData { get; private set; }
    private const string SAVEDATA = "SAVE_DATA";

    private void Awake()
    {
        Instace = this;
        DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.HasKey(SAVEDATA))
        {
            SaveData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SAVEDATA));
            Debug.Log("Has");
            Debug.Log(SaveData.SoberManUnlocked);
            Debug.Log(SaveData.UnlockedRecipies.Count);
        }
        else
        {
            SaveData = new SaveData();
        }
        SceneManager.sceneLoaded += OnSceneLoadded;

    }

    private void OnSceneLoadded(Scene scene, LoadSceneMode arg1)
    {
        if (scene.buildIndex == 1)
        {
            string json = JsonUtility.ToJson(SaveData);
            PlayerPrefs.SetString(SAVEDATA, json);
            PlayerPrefs.Save();
            Debug.Log("Saved");
            Debug.Log(SaveData.SoberManUnlocked);
            Debug.Log(SaveData.UnlockedRecipies.Count);
        }
    }

    public PlayerData CurrentPlayer;


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoadded;
        string json = JsonUtility.ToJson(SaveData);
        
        PlayerPrefs.SetString(SAVEDATA, json);
        PlayerPrefs.Save();
        Debug.Log("Saved");
        Debug.Log(SaveData.SoberManUnlocked);
        Debug.Log(SaveData.UnlockedRecipies.Count);
    }

    [ContextMenu ("Reset")]
    

    public void Reset()
    {
        PlayerPrefs.DeleteKey(SAVEDATA);
    }
}
