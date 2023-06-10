using UnityEngine;
using TMPro;
using DG.Tweening;

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
        _scoreText.transform.DOScale(1.3f,0.2f).OnComplete(() => { _scoreText.transform.DOScale(1.1f, 0.1f); });
        
    }
    private void OnDisable()
    {
        HumanPlayer.ScoreChanged -= OnScoreLevelChanged;
    }
}

