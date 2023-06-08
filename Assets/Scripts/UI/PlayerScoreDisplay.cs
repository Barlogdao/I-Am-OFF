using UnityEngine;
using TMPro;

public class PlayerScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        HumanPlayer.ScoreChanged += OnScoreLevelChanged;
    }
    private void Start()
    {
        _scoreText.text = 0.ToString();
    }

    private void OnScoreLevelChanged(int newScore)
    {
        _scoreText.text = newScore.ToString();
    }
    private void OnDisable()
    {
        HumanPlayer.ScoreChanged -= OnScoreLevelChanged;
    }
}

