using UnityEngine;

public class BestScoreDisplay : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI _bestScore;
    
    private void Start()
    {

        int score = SaveProvider.Instace.SaveData.MaxScore;
        if (score <= 0)
        {
            gameObject.SetActive(false);
        }
        _bestScore.text =score.ToString();
    }

}
