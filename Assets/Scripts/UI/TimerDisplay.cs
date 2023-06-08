using UnityEngine;
using TMPro;

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
    }

    private void OnDisable()
    {
        Game.TimerTicked -= OnTick;
        Game.GameOvered -= OnGameOver;
    }
}
