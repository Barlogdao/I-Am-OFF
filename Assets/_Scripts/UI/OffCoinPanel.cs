using UnityEngine;

public class OffCoinPanel : MonoBehaviour
{
    [SerializeField] GameObject _offCoinPrefab;
    private void OnEnable()
    {
        HumanPlayer.PlayerMindChanged += OnPlayerOFF;
    }

    private void OnPlayerOFF(bool playerIsOFF)
    {
        if (playerIsOFF == true)
        {
            Instantiate(_offCoinPrefab, transform);
        }
    }
    private void OnDisable()
    {
        HumanPlayer.PlayerMindChanged -= OnPlayerOFF;
    }

    public void DestroyChild()
    {
        if(transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
