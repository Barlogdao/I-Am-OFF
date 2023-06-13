using UnityEngine;

public class BestScoreDisplay : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI _bestScore;
    private const string PLAYER_SCORE = "PLAYER_SCORE";
    private void Start()
    {
        int score = PlayerPrefs.GetInt(PLAYER_SCORE, 0);
        if (score <= 0)
        {
            gameObject.SetActive(false);
        }
        _bestScore.text = $"Best Score:\n{score}";
    }

}
