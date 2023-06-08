using UnityEngine;
using TMPro;
using DG.Tweening;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timer;

    private void OnEnable()
    {
        Game.TimerTicked += OnTick;
        Game.GameOvered += OnGameOver;
    }

    private void OnGameOver()
    {
        _timer.enabled = false;
    }

    private void OnTick(int timeleft)
    {
        _timer.text = timeleft.ToString();
        _timer.transform.DOScale(1.1f, 0.3f).OnComplete(() => _timer.transform.DOScale(1f, 0.3f));
    }

    private void OnDisable()
    {
        Game.TimerTicked -= OnTick;
        Game.GameOvered -= OnGameOver;
    }
}
