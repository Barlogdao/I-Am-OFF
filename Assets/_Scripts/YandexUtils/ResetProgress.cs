using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class ResetProgress : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SaveProvider.Instace.Reset();
        YandexGame.ResetSaveProgress();
        Debug.Log("Нажато");
    }
}
