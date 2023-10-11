using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private int _gameTimer;
    private PlayerData _playerData;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private DrinkWheel _drinkWheel;
    private readonly WaitForSeconds _tick = new WaitForSeconds(1f);

    public static event Action<int> TimerTicked;

    public static event Action GameStarted;

    public static event Action GameOvered;

    public static Game Instance { get; private set; }

    public GameState State { get; private set; }

    public HumanPlayer Player { get; private set; }

    public float HangoverTime { get; private set; }

    [field: SerializeField] public int OffStrikeBonusScore { get; private set; }

    public void AddBonusTime(int bonusTime)
    {
        _gameTimer += bonusTime;
        TimerTicked?.Invoke(_gameTimer);
    }

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
        CreateDrinks();

        yield return null;

        StartCoroutine(StartGame());
    }

    private void InitPlayer()
    {
        Player = FindObjectOfType<HumanPlayer>();
        Player.Init(_playerData);
    }

    private void CreateDrinks()
    {
        _drinkWheel.Init(this);
    }

    private IEnumerator StartGame()
    {
        State = GameState.Play;
        GameStarted?.Invoke();        
        TimerTicked?.Invoke(_gameTimer);

        while (_gameTimer > 0)
        {
            yield return _tick;
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
    GameOver,
}
