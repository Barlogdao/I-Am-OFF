using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    private List<PlayerData> _playerList = new();
    private PlayerProvider _playerProvider;
    private SaveData _saveData;

    [SerializeField] GameObject _leftArrow, _rightArrow;

    private PlayerData _currentPlayer;
    private int _currentPlayerIndex;
    [SerializeField] Animator _playerVisual;

    private void Start()
    {
        _playerProvider = PlayerProvider.Instance;
        _saveData = SaveProvider.Instace.SaveData;

        foreach (PlayerData playerData in _playerProvider.GetAllPlayers())
        {
            if (_saveData.IsPlayerUnlocked(playerData.ID))
            {
                _playerList.Add(playerData);
            }
        }

        CheckPlayersCount();

        _currentPlayer = _playerProvider.GetPlayer(_saveData.CurrentPlayerID);
        _currentPlayerIndex = _playerList.IndexOf(_currentPlayer);
        UpdateCurrentPlayer();
    }

    private void CheckPlayersCount()
    {
        if (_playerList.Count < 2)
        {
            _leftArrow.SetActive(false);
            _rightArrow.SetActive(false);
        }
        else
        {
            _leftArrow.SetActive(true);
            _rightArrow.SetActive(true);
        }
    }

    private void OnEnable()
    {
        PlayerProvider.PlayerChanged += OnPlayerChanged;
    }

    private void OnPlayerChanged()
    {
        if (_playerList.Contains(_playerProvider.GetPlayer(_saveData.CurrentPlayerID)) == false)
        {
            _playerList.Add(_playerProvider.GetPlayer(_saveData.CurrentPlayerID));
            CheckPlayersCount();
        }

        UpdateCurrentPlayer();
    }

    private void OnDisable()
    {
        PlayerProvider.PlayerChanged -= OnPlayerChanged;
    }

    private void UpdateCurrentPlayer()
    {
        _currentPlayer = _playerProvider.GetPlayer(_saveData.CurrentPlayerID);
        _playerVisual.runtimeAnimatorController = _currentPlayer.Animator;
    }

    public void Next()
    {
        _currentPlayerIndex++;

        if (_currentPlayerIndex >= _playerList.Count)
        {
            _currentPlayerIndex = 0;
        }

        _saveData.CurrentPlayerID = _playerList[_currentPlayerIndex].ID;
        UpdateCurrentPlayer();

    }
    public void Previous()
    {
        _currentPlayerIndex = (_currentPlayerIndex - 1) < 0 ? _playerList.Count - 1 : _currentPlayerIndex - 1;
        _saveData.CurrentPlayerID = _playerList[_currentPlayerIndex].ID;

        UpdateCurrentPlayer();
    }

}
