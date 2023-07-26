using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinAmount;

    private void OnEnable()
    {
        UpdateCoinVisual(SaveProvider.Instace.SaveData.PlayerCoins);
        SaveProvider.CoinAmountChanged += UpdateCoinVisual;
    }

    private void OnDisable()
    {
        SaveProvider.CoinAmountChanged -= UpdateCoinVisual;
    }

    private void UpdateCoinVisual(int amount)
    {
        _coinAmount.text = $" x {amount}";
    }
}
