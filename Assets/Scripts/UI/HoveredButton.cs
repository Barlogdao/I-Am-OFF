using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoveredButton : MonoBehaviour, IPointerEnterHandler
{
    public static event Action ButtonHovered;
    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonHovered?.Invoke();
    }
}
