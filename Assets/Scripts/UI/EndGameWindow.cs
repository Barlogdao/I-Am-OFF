using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

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
        
        StartCoroutine(GameEndProcess());
        
    }
    private void OnDestroy()
    {
        Game.GameOvered -= OnGameOvered;
    }

    private IEnumerator GameEndProcess()
    {
        _restartButton.enabled = false;
        _exitButton.enabled = false;
        var coinPanel = FindObjectOfType<OffCoinPanel>();
        int newScore = Game.Instance.Player.Score;
        int oldScore = PlayerPrefs.GetInt(PLAYER_SCORE, 0);
        _gameScore.text = newScore.ToString();
        _bestScore.enabled = false;
        yield return new WaitForSeconds(0.2f);
        for (int i = 1; i <= Game.Instance.Player.DrunkStrikeCount; i++)
        {
            newScore += Game.Instance.OffStrikeBonusScore * i;
            _gameScore.text = newScore.ToString();
            
            coinPanel.DestroyChild();
            yield return new WaitForSeconds(0.3f);
        }
        _bestScore.enabled = true;
        if (oldScore < newScore)
        {
            _bestScore.text = $" New Best Score: {newScore}";
            PlayerPrefs.SetInt(PLAYER_SCORE, newScore);
        }
        else
        {
            _bestScore.text = $"Best Score: {oldScore}";
        }
        _restartButton.enabled = true;
        _exitButton.enabled = true;
    }
}
