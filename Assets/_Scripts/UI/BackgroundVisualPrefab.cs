using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackgroundVisualPrefab : UnlockablePrefab, IPointerClickHandler
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private GameObject _lock;

    [SerializeField] private Image _frame;
    [SerializeField] private UnlockConditionVisual _unlockConditionVisual;

    private BackgroundSO _background;
    private SaveData _data;
    private Color _originFrameColor;
    public void Init(BackgroundSO background)
    {
        _data = SaveProvider.Instace.SaveData;
        _background = background;
        _backgroundImage.sprite = _background.Image;
        _originFrameColor = _frame.color;

        if (_data.IsBackgroundUnlocked(_background.ID))
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

    private void OnEnable()
    {
        BackGroundProvider.BackgroundChanged += OnBackGroundChanged;
    }
    private void OnDisable()
    {
        BackGroundProvider.BackgroundChanged -= OnBackGroundChanged;
    }

    private void OnBackGroundChanged()
    {
        if (_data.CurrentBackGroundID == _background.ID)
        {
            _frame.color = Color.green;
        }
        else
        {
            _frame.color = _originFrameColor;
        }
    }

    private void ShowLock()
    {
        _lock.SetActive(true);

        if (_background.EarnType == EarnType.Basic)
        {
            _lock.SetActive(false);
            _data.UnlockBackground(_background.ID);
        }
        else
        {
            Instantiate(_unlockConditionVisual, _lock.transform).Initialize(_background);
        }
    }

    public void TryUnlockElement()
    {
        if (_data.IsBackgroundUnlocked(_background.ID))
            return;

        switch (_background.EarnType)
        {
            case EarnType.Purchase:
                if (_data.PlayerCoins >= _background.CoinCost)
                {
                    SaveProvider.Instace.SpendCoins(_background.CoinCost);
                    UnlockProcess();
                }
                break;

            case EarnType.Reward:
                RewardProvider.RewardRequested?.Invoke(UnlockProcess);
                break;
            case EarnType.Trigger:
                break;
        }
    }

    public void SetBackgroundAsCurrent()
    {
        _data.CurrentBackGroundID = _background.ID;
        
        BackGroundProvider.BackgroundChanged?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_data.CurrentBackGroundID != _background.ID)
        SetBackgroundAsCurrent();
    }

    private void UnlockProcess()
    {
        _data.UnlockBackground(_background.ID);
        RaisePurchaseEvent();
        HideLock();
    }
}
