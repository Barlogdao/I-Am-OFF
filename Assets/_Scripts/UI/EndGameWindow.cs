using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using Assets.SimpleLocalization.Scripts;
using YG;
public class EndGameWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameScore;
    [SerializeField] private TextMeshProUGUI _bestScore;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TextMeshProUGUI _coinAmountText;

    private void Awake()
    {
        _exitButton.onClick.AddListener(() => OnExitGame());
        Game.GameOvered += OnGameOvered;
        gameObject.SetActive(false);
    }

    private void OnExitGame()
    {
        SaveProvider.Instace.AddCoins(Game.Instance.Player.EarnedCoins);
        SceneManager.LoadScene(1);
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
        SaveProvider saveProvider = SaveProvider.Instace;
        HumanPlayer player = Game.Instance.Player;
        int coinsEarned = player.EarnedCoins;

        _exitButton.gameObject.SetActive(false);
        var coinPanel = FindObjectOfType<OffCoinPanel>();
        int newScore = player.Score;
        int oldScore = saveProvider.SaveData.MaxScore;
        coinPanel.gameObject.SetActive(false);
        _coinAmountText.text = $" x {coinsEarned}";

        _gameScore.text = newScore.ToString();
        _bestScore.enabled = false;
        yield return new WaitForSeconds(1f);

        _bestScore.enabled = true;
        if (oldScore < newScore)
        {
            _bestScore.text = $" {LocalizationManager.Localize("Game.NewBestScore")}{newScore}";
            saveProvider.ChangeMaxScore(newScore);
        }
        else
        {
            _bestScore.text = $"{LocalizationManager.Localize("Game.BestScore")}{oldScore}";
        }

        if (oldScore < newScore)
        {
            YandexGame.NewLeaderboardScores("DrinkLeaderBoard", newScore);
        }

        _exitButton.gameObject.SetActive(true);
    }
}
