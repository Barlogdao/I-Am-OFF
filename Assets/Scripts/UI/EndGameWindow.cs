using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class EndGameWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameScore;
    [SerializeField] private TextMeshProUGUI _bestScore;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    private const string PLAYER_SCORE = "PLAYER_SCORE";

    public static event Action ChampionUnlocked;
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
        _restartButton.gameObject.SetActive(false);
        _exitButton.gameObject.SetActive(false);
        var coinPanel = FindObjectOfType<OffCoinPanel>();
        int newScore = Game.Instance.Player.Score;
        int oldScore = PlayerPrefs.GetInt(PLAYER_SCORE, 0);
        _gameScore.text = newScore.ToString();
        _bestScore.enabled = false;
        yield return new WaitForSeconds(0.5f);
        for (int i = 1; i <= Game.Instance.Player.DrunkStrikeCount; i++)
        {
            newScore += Game.Instance.OffStrikeBonusScore * i;
            _gameScore.text = newScore.ToString();
            
            coinPanel.DestroyChild();
            yield return new WaitForSeconds(0.5f);
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
        if(!SaveProvider.Instace.SaveData.SoberManUnlocked && newScore <= 0)
        {
            SaveProvider.Instace.SaveData.SoberManUnlocked = true;
            ChampionUnlocked?.Invoke();
        }
        if(!SaveProvider.Instace.SaveData.AlkodzillaUnlocked && Game.Instance.Player.DrunkStrikeCount >= 5)
        {
            SaveProvider.Instace.SaveData.AlkodzillaUnlocked = true;
            ChampionUnlocked?.Invoke();
        }
        _restartButton.gameObject.SetActive(true);
        _exitButton.gameObject.SetActive(true);
    }
}
