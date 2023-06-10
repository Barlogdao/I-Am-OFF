using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CoctailCanvas : MonoBehaviour
{
    [SerializeField] Image _coctailImage;
    [SerializeField] TextMeshProUGUI _score, _coctailName;

    private void Start()
    {
        _coctailImage.enabled = false;
        _score.enabled = false;
        _coctailName.enabled = false;
    }

    private void OnEnable()
    {
        HumanPlayer.CoctailDrinked += OnCoctailDrinked;
    }

    private void OnCoctailDrinked(CoctailRecipeSO coctail)
    {
        _coctailImage.enabled = true;
        _score.enabled = true;
        _coctailName.enabled = true;
        _coctailImage.sprite = coctail.Image;
        _score.text = $"+{coctail.BonusScore}";
        _coctailName.text = coctail.Name;
        _score.transform.DOScale(1.2f, 0.5f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            _score.transform.localScale = Vector3.one;
            _coctailImage.enabled = false;
            _score.enabled = false;
        });
        _coctailName.transform.DOScale(1.2f,0.5f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            _coctailName.transform.localScale = Vector3.one;
            _coctailName.enabled = false;
        });



    }

    private void OnDisable()
    {
        HumanPlayer.CoctailDrinked -= OnCoctailDrinked;
    }
}
