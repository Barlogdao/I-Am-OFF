using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DrunkMeterDisplay : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _flowingCoin;
    [SerializeField] private Image _filler;
    [SerializeField] private Color _soberColor;
    [SerializeField] private Color _drunkColor;
    [SerializeField] private Color _drunkAsHellColor;

    [SerializeField] RectTransform _coinTransform;
    [SerializeField] private float _flowDuration;
    [SerializeField] private float _flowForce;

    private void OnEnable()
    {
        HumanPlayer.DrunkLevelChanged += OnDrunkLevelChanged;
        HumanPlayer.SobrietyChanged += OnSobrietyChanged;
        HumanPlayer.PlayerMindChanged += OnPlayerMindChanged;
    }

    private void OnPlayerMindChanged(bool playerIsOFF)
    {
        if (playerIsOFF)
        {
            _slider.DOValue(0,Game.Instance.HangoverTime - 0.05f).SetEase(Ease.Linear);
            _flowingCoin.enabled = false;
        }
    }

    private void OnSobrietyChanged(SobrietyLevel soberLevel)
    {
        switch (soberLevel)
        {
            case SobrietyLevel.Sober:
                _filler.DOColor(_soberColor, 0.3f);
                _flowingCoin.enabled = true;
                break;
            case SobrietyLevel.Drunk:
                _filler.DOColor(_drunkColor, 0.3f);
                break;
            case SobrietyLevel.DrunkAsHell:
                _filler.DOColor(_drunkAsHellColor, 0.3f);
                break;
        }
    }

    private void OnDrunkLevelChanged(float newValue)
    {
        _slider.DOValue(newValue, 0.4f);
    }

    private void Start()
    {
        _slider.value = 0;
        _coinTransform.DOAnchorPosY(_flowForce, _flowDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
    }
    private void OnDisable()
    {
        HumanPlayer.DrunkLevelChanged -= OnDrunkLevelChanged;
        HumanPlayer.SobrietyChanged -= OnSobrietyChanged;
        HumanPlayer.PlayerMindChanged -= OnPlayerMindChanged;
    }


}
