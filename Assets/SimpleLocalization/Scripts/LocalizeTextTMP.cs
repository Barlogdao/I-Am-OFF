using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(TextMeshProUGUI))]

public class LocalizeTextTMP : MonoBehaviour
{
    public string LocalizationKey;

    public void Start()
    {
        Localize();
        LocalizationManager.LocalizationChanged += Localize;
    }

    public void OnDestroy()
    {
        LocalizationManager.LocalizationChanged -= Localize;
    }

    private void Localize()
    {
        GetComponent<TextMeshProUGUI>().text = LocalizationManager.Localize(LocalizationKey);
    }
}
