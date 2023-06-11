using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{

    public List<PlayerData> PlayerList;
    private PlayerData _currentPlayer;
    private int _currentPlayerIndex;
    [SerializeField] Animator _playerVisual;
    [SerializeField] TMPro.TextMeshProUGUI _playerName;
    [SerializeField] PlayerData _soberMan;
    [SerializeField] PlayerData _alkodzoilla;

    private void Start()
    {
        if (SaveProvider.Instace.SaveData.SoberManUnlocked)
        {
            PlayerList.Add(_soberMan);
        }
        if (SaveProvider.Instace.SaveData.AlkodzillaUnlocked)
        {
            PlayerList.Add(_alkodzoilla);
        }
        _currentPlayerIndex = 0;
        SetNewPlayer();
    }
    private void SetNewPlayer()
    {
        _currentPlayer = PlayerList[_currentPlayerIndex];
        SaveProvider.Instace.CurrentPlayer = _currentPlayer;
        _playerVisual.runtimeAnimatorController = _currentPlayer.Animator;
        _playerName.text = _currentPlayer.Name;
    }

    public void Next()
    {
        _currentPlayerIndex++;
        if (_currentPlayerIndex >= PlayerList.Count)
        {
            _currentPlayerIndex = 0;
        }
        SetNewPlayer();

    }
    public void Previous()
    {
        _currentPlayerIndex = (_currentPlayerIndex - 1) < 0 ? PlayerList.Count - 1 : _currentPlayerIndex - 1;
        SetNewPlayer();
    }

}
