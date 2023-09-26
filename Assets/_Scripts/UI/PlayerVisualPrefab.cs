using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.SimpleLocalization.Scripts;
using System;

public class PlayerVisualPrefab : UnlockablePrefab, IPointerClickHandler
{
    [SerializeField] private Image _playerImage;


    [SerializeField] private Image _frame;
    [SerializeField] private GameObject _lock;
    [SerializeField] private UnlockConditionVisual _unlockConditionVisual;
    [SerializeField] private TMPro.TextMeshProUGUI _playerLabel;
    [SerializeField] private CharacteristicSlider _staminaSlider, _alcoholResistanceSlider, _hangoverResistanceSlider;

    private PlayerData _playerData;
    private SaveData _saveData;
    private Color _originFrameColor;

    public static event Action NewPlayerSelected;
    public void Initialize(PlayerData data)
    {
        _playerData = data;
        _saveData = SaveProvider.Instace.SaveData;
        _playerImage.sprite = _playerData._visualImage;
        _staminaSlider.Initialize(_playerData.StaminaValue);
        _alcoholResistanceSlider.Initialize(_playerData.AlcoholResistanceValue);
        _hangoverResistanceSlider.Initialize(_playerData.HangoverResistanceValue);
        _originFrameColor = _frame.color;
        _playerLabel.text = LocalizationManager.Localize(_playerData.Name);

        if (_saveData.IsPlayerUnlocked(_playerData.ID))
        {
            HideLock();
        }
        else
        {
            ShowLock();
        }
    }

    private void HideLock()
    {
        _lock.SetActive(false);
    }

    private void ShowLock()
    {
        _lock.SetActive(true);

        if (_playerData.EarnType == EarnType.Basic)
        {
            _lock.SetActive(false);
            _saveData.UnlockPlayer(_playerData.ID);
        }
        else
        {
            Instantiate(_unlockConditionVisual, _lock.transform).Initialize(_playerData);
        }
    }

    public void TryUnlockElement()
    {
        if (_saveData.IsPlayerUnlocked(_playerData.ID))
            return;

        switch (_playerData.EarnType)
        {
            case EarnType.Purchase:
                if (_saveData.PlayerCoins >= _playerData.CoinCost)
                {
                    SaveProvider.Instace.SpendCoins(_playerData.CoinCost);
                    _saveData.UnlockPlayer(_playerData.ID);
                    RaisePurchaseEvent();
                    HideLock();
                }
                break;

            case EarnType.Reward:
                break;
            case EarnType.Trigger:
                break;
        }
    }

    private void OnEnable()
    {
        PlayerProvider.PlayerChanged += OnPlayerChanged;
    }

    private void OnPlayerChanged()
    {
        if (_saveData.CurrentPlayerID == _playerData.ID)
        {
            _frame.color = Color.green;
        }
        else
        {
            _frame.color = _originFrameColor;
        }
    }

    private void OnDisable()
    {
        PlayerProvider.PlayerChanged -= OnPlayerChanged;
    }

    public void SetPlayerAsCurrent()
    {
        _saveData.CurrentPlayerID = _playerData.ID;
        NewPlayerSelected?.Invoke();
        PlayerProvider.PlayerChanged?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetPlayerAsCurrent();
    }

}
