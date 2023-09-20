using UnityEngine;

public class BackgroundMenu : MonoBehaviour
{
    [SerializeField] private BackgroundVisualPrefab _visualPrefab;

    private void Start()
    {
        foreach (var background in BackGroundProvider.Instance.GetAllBackgrounds())
        {
            Instantiate(_visualPrefab, transform).Init(background);
        }
    }
}
