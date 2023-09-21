using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Assets.SimpleLocalization.Scripts;

public class CoctailCanvas : MonoBehaviour
{
    [SerializeField] Image _coctailImage;
    [SerializeField] TextMeshProUGUI _score, _coctailName;
    [SerializeField] float _showDuration;

    private void Start()
    {
        _coctailImage.enabled = false;
        _score.enabled = false;
        _coctailName.enabled = false;
    }

    private void OnEnable()
    {
        HumanPlayer.CoctailDrinked += OnCocktailDrinked;
    }

    private void OnCocktailDrinked(CocktailRecipeSO cocktail)
    {
        _coctailImage.enabled = true;
        _score.enabled = true;
        _coctailName.enabled = true;
        _coctailImage.sprite = cocktail.Image;
        _score.text = $"+{cocktail.BonusScore}";
        _coctailName.text = LocalizationManager.Localize($"Cocktail.{cocktail.Name}");
        _score.transform.DOScale(1.2f, _showDuration).SetEase(Ease.OutSine).OnComplete(() =>
        {
            _score.transform.localScale = Vector3.one;
            _coctailImage.enabled = false;
            _score.enabled = false;
        });
        _coctailName.transform.DOScale(1.2f, _showDuration).SetEase(Ease.OutSine).OnComplete(() =>
        {
            _coctailName.transform.localScale = Vector3.one;
            _coctailName.enabled = false;
        });



    }

    private void OnDisable()
    {
        HumanPlayer.CoctailDrinked -= OnCocktailDrinked;
    }
}
