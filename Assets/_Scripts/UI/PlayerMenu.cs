using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] private PlayerVisualPrefab _visualPrefab;
    private void Start()
    {
        foreach (var playerData in PlayerProvider.Instance.GetAllPlayers())
        {
            Instantiate(_visualPrefab, transform).Initialize(playerData);
        }
        PlayerProvider.PlayerChanged?.Invoke();
    }
}
