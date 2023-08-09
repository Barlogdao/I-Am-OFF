using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveProvider : MonoBehaviour
{
    private const string SAVEDATA = "SAVE_DATA";
    public static SaveProvider Instace { get; private set; }
    public SaveData SaveData { get; private set; }
    public PlayerData CurrentPlayer;
    private ISaveSystem _saveSystem;
    public static event Action<int> CoinAmountChanged;


    private void Awake()
    {
        Instace = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        _saveSystem = new YandexSaveSystem();
        SaveData = _saveSystem.Load();
        SceneManager.sceneLoaded += OnSceneLoadded;
    }

    private void OnSceneLoadded(Scene scene, LoadSceneMode arg1)
    {
        if (scene.buildIndex == 1)
        {
            _saveSystem.Save(SaveData);

        }
    }

    public void SpendCoins (int cost)
    {
        SaveData.PlayerCoins -= cost;
        CoinAmountChanged?.Invoke(SaveData.PlayerCoins);
        _saveSystem.Save(SaveData);
    }
    public void AddCoins(int amount)
    {
        SaveData.PlayerCoins += amount;
        CoinAmountChanged?.Invoke(SaveData.PlayerCoins);
        _saveSystem.Save(SaveData);
    }

    public void ChangeMaxScore(int score)
    {
        SaveData.MaxScore = score;
        _saveSystem.Save(SaveData);
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoadded;
        _saveSystem.Save(SaveData);
    }

    [ContextMenu ("Reset")]
    

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
