using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class Game : MonoBehaviour
{

    [SerializeField] private  int _gameTimer;
    public static Game Instance { get; private set; }
    public GameState State {  get; private set; }
    [field: SerializeField] public LayerMask PlayerMask { get; private set; }
    public HumanPlayer Player { get; private set; }
    [field: SerializeField] public float PlayerOFFTime { get; private set; }
    [field: SerializeField] public int OffStrikeBonusScore { get; private set; }
    [SerializeField] PlayerData _playerData;

    public static event Action<int> TimerTicked;
    public static event Action GameStarted;
    public static event Action GameOvered;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _playerData = SaveProvider.Instace.CurrentPlayer;
        PlayerOFFTime = _playerData.OFFTime;
        State = GameState.Init;
        StartCoroutine(InitGame());
    }


    private IEnumerator InitGame()
    {
        InitPlayer();
        yield return null;
        InitDrinks();
        StartCoroutine(StartGame());
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
        int timer = _gameTimer;
        TimerTicked?.Invoke(timer);
        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
            TimerTicked?.Invoke(timer);
        }
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        State = GameState.GameOver;
        GameOvered?.Invoke();
        yield return null;
    }

}



public enum GameState
{
    Init,
    Play,
    GameOver
}
