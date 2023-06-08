using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class EndGameWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameScore;
    [SerializeField] private TextMeshProUGUI _bestScore;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    private const string PLAYER_SCORE = "PLAYER_SCORE";
    private void Awake()
    {
        _restartButton.onClick.AddListener(() => SceneManager.LoadScene(2));
        _exitButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        Game.GameOvered += OnGameOvered;
        gameObject.SetActive(false);
    }

    private void OnGameOvered()
    {
        gameObject.SetActive(true);
        int newScore = Game.Instance.Player.Score;
        int oldScore = PlayerPrefs.GetInt(PLAYER_SCORE,0);
        _gameScore.text = newScore.ToString();
        if (oldScore < newScore) 
        {
            _bestScore.text = $" New Best Score: {newScore}";
            PlayerPrefs.SetInt(PLAYER_SCORE, newScore);
        }
        else
        {
            _bestScore.text = $"Best Score: {oldScore}";
        }
        
    }
    private void OnDestroy()
    {
        Game.GameOvered -= OnGameOvered;
    }
}
