using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Image))]
public class DisplayBackground : MonoBehaviour
{
    private Image _background;
    private void Start()
    {
        _background = GetComponent<Image>();
        _background.sprite = BackGroundProvider.Instance.GetBackground(SaveProvider.Instace.SaveData.CurrentBackGroundID);
    }
    private void OnEnable()
    {
        BackGroundProvider.BackgroundChanged += OnBackGroundChanged;
    }

    private void OnDisable()
    {
        BackGroundProvider.BackgroundChanged -= OnBackGroundChanged;
    }

    private void OnBackGroundChanged()
    {
        _background.sprite = BackGroundProvider.Instance.GetBackground(SaveProvider.Instace.SaveData.CurrentBackGroundID);
        YandexGame.savesData.CurrentBackgroundID = SaveProvider.Instace.SaveData.CurrentBackGroundID;

    }
}
