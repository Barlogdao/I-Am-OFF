using System;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class HoveredButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public static event Action ButtonHovered;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!YandexGame.EnvironmentData.isDesktop)
        {
            ButtonHovered?.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (YandexGame.EnvironmentData.isDesktop)
        {
            ButtonHovered?.Invoke();
        }
    }
}
