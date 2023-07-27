using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using Assets.SimpleLocalization.Scripts;

public class EndGameWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameScore;
    [SerializeField] private TextMeshProUGUI _bestScore;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TextMeshProUGUI _coinAmountText;

    
    public static event Action ChampionUnlocked;
    private void Awake()
    {
        _exitButton.onClick.AddListener(() => OnExitGame());
        Game.GameOvered += OnGameOvered;
        gameObject.SetActive(false);
    }

    private void OnExitGame()
    {
        SaveProvider.Instace.AddCoins(Game.Instance.Player.DrunkStrikeCount);
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

        _exitButton.gameObject.SetActive(false);
        var coinPanel = FindObjectOfType<OffCoinPanel>();
        int newScore = Game.Instance.Player.Score;
        int oldScore = saveProvider.SaveData.MaxScore;
        _coinAmountText.text = $" x {Game.Instance.Player.DrunkStrikeCount}";

        _gameScore.text = newScore.ToString();
        _bestScore.enabled = false;
        yield return new WaitForSeconds(1f);
        for (int i = 1; i <= Game.Instance.Player.DrunkStrikeCount; i++)
        {
            newScore += Game.Instance.OffStrikeBonusScore * i;
            _gameScore.text = newScore.ToString();
            
            coinPanel.DestroyChild();
            yield return new WaitForSeconds(1f);
        }
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
        if(!saveProvider.SaveData.SoberManUnlocked && newScore <= 0)
        {
            saveProvider.SaveData.SoberManUnlocked = true;
            ChampionUnlocked?.Invoke();
        }
        if(!saveProvider.SaveData.AlkodzillaUnlocked && Game.Instance.Player.DrunkStrikeCount >= 3)
        {
            saveProvider.SaveData.AlkodzillaUnlocked = true;
            ChampionUnlocked?.Invoke();
        }

        _exitButton.gameObject.SetActive(true);
    }
}
