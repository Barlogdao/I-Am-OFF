using System.Collections;
using UnityEngine;
using DG.Tweening;

public class OffCoinPanel : MonoBehaviour
{
    [SerializeField] GameObject _offCoinPrefab;
    [SerializeField] Transform _coinStartPosition;
    [SerializeField] Transform _coinImage;
    private void OnEnable()
    {
        HumanPlayer.PlayerMindChanged += OnPlayerOFF;
        _coinImage.position = _coinStartPosition.position;
        _coinImage.gameObject.SetActive(false);
    }

    private void OnPlayerOFF(bool playerIsOFF)
    {
        if (playerIsOFF == true)
        {
            _coinImage.gameObject.SetActive(true);
            _coinImage.DOMove(transform.position, 1f).SetEase(Ease.InBack).OnComplete(() =>
            {
                Instantiate(_offCoinPrefab, transform);
                _coinImage.position = _coinStartPosition.position;
                _coinImage.gameObject.SetActive(false);
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
