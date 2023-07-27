using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Unity.Collections.LowLevel.Unsafe;

public class DrunkMeterDisplay : MonoBehaviour
{
    [SerializeField] private Image _filler;
    [SerializeField] private Color _soberColor;
    [SerializeField] private Color _drunkColor;
    [SerializeField] private Color _drunkAsHellColor;

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
            _filler.DOFillAmount(0, Game.Instance.PlayerOFFTime - 0.05f).SetEase(Ease.Linear);
        }
    }

    private void OnSobrietyChanged(SobrietyLevel soberLevel)
    {
        switch (soberLevel)
        {
            case SobrietyLevel.Sober:
                _filler.DOColor(_soberColor, 0.3f);
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
        _filler.DOFillAmount(newValue, 0.4f);
    }

    private void Start()
    {
        _filler.fillAmount = 0;
    }
    private void OnDisable()
    {
        HumanPlayer.DrunkLevelChanged -= OnDrunkLevelChanged;
        HumanPlayer.SobrietyChanged -= OnSobrietyChanged;
        HumanPlayer.PlayerMindChanged -= OnPlayerMindChanged;
    }


}
