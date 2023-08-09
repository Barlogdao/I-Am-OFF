using UnityEngine;
using TMPro;
using DG.Tweening;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timer;
    private int _currentTimeLeft = 0;
    private Color _originColor;

    private void OnEnable()
    {
        Game.TimerTicked += OnTick;
        Game.GameOvered += OnGameOver;
        _originColor = _timer.color;    
    }

    

    private void OnGameOver()
    {
        _timer.enabled = false;
    }

    private void OnTick(int timeLeft)
    {
        if (_currentTimeLeft < timeLeft)
        {
            _timer.DOColor(Color.green, 0.6f).From().OnComplete(() => _timer.color = _originColor);
        }
        _currentTimeLeft = timeLeft;
        _timer.text = timeLeft.ToString();
        _timer.transform.DOScale(1.1f, 0.3f).OnComplete(() => _timer.transform.DOScale(1f, 0.3f));
    }

    private void OnDisable()
    {
        Game.TimerTicked -= OnTick;
        Game.GameOvered -= OnGameOver;
    }
}
