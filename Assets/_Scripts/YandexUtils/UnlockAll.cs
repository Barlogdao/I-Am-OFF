using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class UnlockAll : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SaveProvider.Instace.UnlockAll();
    }
}
