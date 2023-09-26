using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    private int _gameTimer;
    [SerializeField] private GameConfig _gameConfig;
    public static Game Instance { get; private set; }
    public GameState State {  get; private set; }
    public HumanPlayer Player { get; private set; }
    public float HangoverTime { get; private set; }
    [field: SerializeField] public int OffStrikeBonusScore { get; private set; }
    private PlayerData _playerData;

    public static event Action<int> TimerTicked;
    public static event Action GameStarted;
    public static event Action GameOvered;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _playerData = PlayerProvider.Instance.GetPlayer(SaveProvider.Instace.SaveData.CurrentPlayerID);
        HangoverTime = _playerData.GetHangoverTime(_gameConfig);
        State = GameState.Init;
        StartCoroutine(InitGame());
    }


    private IEnumerator InitGame()
    {
        _gameTimer = _gameConfig.GameDuration;
        InitPlayer();
        yield return null;
        InitDrinks();
        StartCoroutine(StartGame());
    }

    public void AddBonusTime(int bonusTime)
    {
        _gameTimer += bonusTime;
        TimerTicked?.Invoke(_gameTimer);
    }

    private void InitPlayer()
    {
        Player = FindObjectOfType<HumanPlayer>();
        Player.Init(_playerData);
    }
    private void InitDrinks()
    {
        foreach (var drink in FindObjectsByType<Drink>(FindObjectsSortMode.None))
        {
            drink.Init(this);
        }
    }

    private IEnumerator StartGame()
    {
        State = GameState.Play;

        GameStarted?.Invoke();
        
        TimerTicked?.Invoke(_gameTimer);
        while (_gameTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            _gameTimer--;
            TimerTicked?.Invoke(_gameTimer);
        }
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        State = GameState.GameOver;
        GameOvered?.Invoke();
        yield return null;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
    }
}

public enum GameState
{
    Init,
    Play,
    GameOver
}
