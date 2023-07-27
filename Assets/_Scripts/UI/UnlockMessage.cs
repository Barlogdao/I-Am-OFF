using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using Assets.SimpleLocalization.Scripts;

public class UnlockMessage : MonoBehaviour
{
    private TextMeshProUGUI _unlockText;

    private void Awake()
    {
        _unlockText = GetComponent<TextMeshProUGUI>();

    }
    private void Start()
    {
        _unlockText.enabled = false;
    }
    private void OnEnable()
    {
        EndGameWindow.ChampionUnlocked += OnChampinoUnlocked;
       
    }

    private void OnCoctailUnlocked()
    {
        _unlockText.enabled = true;
        _unlockText.text = "New Coctail Unlocked!";
        _unlockText.transform.DOScale(1.5f, 1f).OnComplete(() => _unlockText.enabled = false);
    }

    private void OnChampinoUnlocked()
    {
        _unlockText.enabled = true;
        _unlockText.text = LocalizationManager.Localize("Game.DrinkerUnlocked");
        _unlockText.transform.DOScale(1.5f,1.5f).OnComplete(()=> _unlockText.enabled=false);

    }

    private void OnDisable()
    {
        EndGameWindow.ChampionUnlocked -= OnChampinoUnlocked;
    }
}
