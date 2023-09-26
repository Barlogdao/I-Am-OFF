using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class OffCoinPanel : MonoBehaviour
{
    [SerializeField] private GameObject _offCoinPrefab;
                   
    [SerializeField] private RectTransform _coinTransform;
    [SerializeField] private Image _coinImage;
    [SerializeField] private float _transitionDiration;
    [SerializeField] private float _maxCoinScale;
    private Vector3 _startCoinScale;


   
    private void OnEnable()
    {
        HumanPlayer.PlayerMindChanged += OnPlayerOFF;
       _coinImage.enabled = false;
        _startCoinScale = _coinTransform.localScale;
    }

    private void OnPlayerOFF(bool playerIsOFF)
    {
        if (playerIsOFF == true)
        {
            _coinImage.enabled = true;

            _coinImage.transform.DOScale(_startCoinScale * _maxCoinScale,_transitionDiration/2)
                .OnComplete(() => _coinImage.transform.DOScale(_startCoinScale, _transitionDiration / 2));
            _coinTransform.DOMove(transform.position, _transitionDiration).SetEase(Ease.InBack)
                .OnComplete(() =>
            {
                Instantiate(_offCoinPrefab, transform);
                _coinTransform.anchoredPosition = Vector2.zero;
                _coinImage.enabled = false;
            });
        }
    }
    private void OnDisable()
    {
        HumanPlayer.PlayerMindChanged -= OnPlayerOFF;
    }

    public void DestroyChild()
    {
        if(transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

}
