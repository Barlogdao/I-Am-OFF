using System;
using UnityEngine;
using UnityEngine.UI;

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
    }

    private void OnSobrietyChanged(SobrietyLevel soberLevel)
    {
        switch (soberLevel)
        {
            case SobrietyLevel.Sober:
                _filler.color = _soberColor;
                break;
            case SobrietyLevel.Drunk:
                _filler.color = _drunkColor;
                break;
            case SobrietyLevel.DrunkAsHell:
                _filler.color = _drunkAsHellColor;
                break;
        }
    }

    private void OnDrunkLevelChanged(float newValue)
    {
        _filler.fillAmount = newValue;
    }

    private void Start()
    {
        _filler.fillAmount = 0;
    }
    private void OnDisable()
    {
        HumanPlayer.DrunkLevelChanged -= OnDrunkLevelChanged;
        HumanPlayer.SobrietyChanged -= OnSobrietyChanged;
    }


}
