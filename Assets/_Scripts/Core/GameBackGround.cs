using Coffee.UIEffects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBackGround : MonoBehaviour
{
    private RawImage _space;
    private UIDissolve _dissolveBar;
    [SerializeField] private float _x,_y;
    private void Awake()
    {
        _space = GetComponentInChildren<RawImage>();
        _dissolveBar = GetComponentInChildren<UIDissolve>();
    }

    private void Start()
    {
        HumanPlayer.SobrietyChanged += OnSobrietyChanged;
        
    }

    private void Update()
    {
        _space.uvRect = new Rect(_space.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _space.uvRect.size);
    }

    private void OnSobrietyChanged(SobrietyLevel level)
    {
        switch (level)
        {
            case SobrietyLevel.Sober:
                _dissolveBar.effectFactor = 0f;
                break;
            case SobrietyLevel.Drunk:
                _dissolveBar.effectFactor = 0f;
                break;
            case SobrietyLevel.DrunkAsHell:
                _dissolveBar.Play();
                break;
        }
    }

    private void OnDestroy()
    {
        HumanPlayer.SobrietyChanged -= OnSobrietyChanged;
    }
}
