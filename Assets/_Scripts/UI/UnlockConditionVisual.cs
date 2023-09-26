using UnityEngine;
using UnityEngine.UI;

public class UnlockConditionVisual : MonoBehaviour
{
    [SerializeField] private Image _unlockVisual;
    [SerializeField] private TMPro.TextMeshProUGUI _coinAmount;
    [SerializeField] private Sprite _purchaseIcon;
    [SerializeField] private Sprite _rewardedIcon;
    [SerializeField] private Sprite _triggerIcon;


    public void Initialize(CollectibleSO element)
    {
        switch (element.EarnType)
        {
            case EarnType.Purchase:
                _unlockVisual.sprite = _purchaseIcon;
                _coinAmount.text = $"x {element.CoinCost}";
                break;

            case EarnType.Reward:
                _unlockVisual.sprite = _rewardedIcon;
                _coinAmount.gameObject.SetActive(false);
                break;

            case EarnType.Trigger:
                _unlockVisual.sprite = _triggerIcon;
                _coinAmount.gameObject.SetActive(false);
                break;
        }
    }

}
