using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.SimpleLocalization.Scripts;

public class RecipeComboVisual : MonoBehaviour
{

    [SerializeField] private Image _coctailImage;
    [SerializeField] private TextMeshProUGUI _coctailName, _bonusScore, _bonusTime;
    [SerializeField] private Transform _comboblock;
    [SerializeField] Image _imagePref;
    [SerializeField] GameObject _lock;
    [SerializeField] Image _coinPrefab;
    private CocktailRecipeSO _cocktail;
    private SaveData _data;

    public void Init(CocktailRecipeSO cocktail)
    {
        _data = SaveProvider.Instace.SaveData;

        _cocktail = cocktail;
        _coctailImage.sprite = cocktail.Image;
        _coctailName.text = LocalizationManager.Localize($"Cocktail.{cocktail.Name}");
        _bonusScore.text = cocktail.BonusScore.ToString();
        _bonusTime.text = cocktail.BonusTime.ToString();

        foreach (var drink in cocktail.Recipe)
        {
            Instantiate(_imagePref, _comboblock).sprite = drink.Image;
        }

        if (_data.IsRecipeUnlocked(_cocktail.ID))
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

        switch (_cocktail.EarnType)
        {
            case EarnType.Basic:
                _lock.SetActive(false);
                _data.UnlockBackground(_cocktail.ID);
                break;

            case EarnType.Purchase:
                for (int i = 0; i < _cocktail.CoinCost; i++)
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

    public void TryUnlockRecipe()
    {
        if (_data.IsRecipeUnlocked(_cocktail.ID))
            return;

        switch (_cocktail.EarnType)
        {
            case EarnType.Purchase:
                if (_data.PlayerCoins >= _cocktail.CoinCost)
                {
                    SaveProvider.Instace.SpendCoins(_cocktail.CoinCost);
                    _data.UnlockRecipe(_cocktail.ID);
                    HideLock();
                }
                break;

            case EarnType.Reward:
                break;

            case EarnType.Trigger:
                break;
        }
    }
}
