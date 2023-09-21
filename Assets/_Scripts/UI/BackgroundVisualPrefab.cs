using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackgroundVisualPrefab : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] GameObject _lock;
    [SerializeField] Image _coinPrefab;
    private BackgroundSO _background;
    private SaveData _data;
    public void Init(BackgroundSO background)
    {
        _data = SaveProvider.Instace.SaveData;

        _background = background;
        _backgroundImage.sprite = _background.Image;

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

    private void ShowLock()
    {
        _lock.SetActive(true);

        switch (_background.EarnType)
        {
            case EarnType.Basic:
                _lock.SetActive(false);
                _data.UnlockBackground(_background.ID);
                break;

            case EarnType.Purchase:
                for (int i = 0; i < _background.CoinCost; i++)
                {
                    Instantiate(_coinPrefab, _lock.transform);
                }
                break;

            case EarnType.Reward:
                break;
            case EarnType.Trigger:
                break;
        }
    }

    public void TryUnlockElement()
    {
        if (_data.IsBackgroundUnlocked(_background.ID))
            return;

        switch (_background.EarnType)
        {
            case EarnType.Purchase:
                if(_data.PlayerCoins >= _background.CoinCost)
                {
                    SaveProvider.Instace.SpendCoins(_background.CoinCost);
                    _data.UnlockBackground(_background.ID);
                    HideLock();
                }
                break;

            case EarnType.Reward:
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
        SetBackgroundAsCurrent();
    }
}
