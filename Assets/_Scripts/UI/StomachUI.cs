using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StomachUI : MonoBehaviour
{
    [SerializeField] private Image _slot1, _slot2, _slot3;
    [SerializeField] Transform _stomachTransform;
    [SerializeField] float _strenght = 1f;
    [SerializeField] float _duration = 0.3f;

    private Image[] slotlist;
    private void Start()
    {
        HumanPlayer.StomachUpdated += OnStomachUpdated;
        slotlist = new Image[] { _slot1, _slot2, _slot3 };
        HideSlots();
    }

    private void OnStomachUpdated(List<DrinkSO> stomach)
    {
        UpdateVisual(stomach.Count, stomach);
    }

    private void OnDestroy()
    {
        HumanPlayer.StomachUpdated -= OnStomachUpdated;
    }

    private void UpdateVisual(int count, List<DrinkSO> stomach)
    {
        _stomachTransform.DOShakePosition(_duration,_strenght);

        if (count == 0) 
        {
            HideSlots();
        }
        else if (count == 1)
        {
            ShowSlot(_slot1, stomach[0].Image);
        }
        else if(count == 2)
        {
            ShowSlot(_slot2 , stomach[1].Image);
        }
        else if( count == 3)
        {
            ShowSlot(_slot3, stomach[2].Image);
        }
    }

    private void ShowSlot(Image slot, Sprite slotImage)
    {
        slot.enabled = true;
        slot.transform.localScale = Vector3.zero;
        slot.transform.DOScale(1f, 0.2f);
        slot.sprite = slotImage;
    }
    private void HideSlots()
    {
        foreach(var slot in slotlist)
        {
            slot.enabled = false;
        }
    }
}
