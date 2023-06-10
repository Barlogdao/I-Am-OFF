using System;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWheel : MonoBehaviour
{

    [SerializeField] float _speed;
    [SerializeField] float _fastSpeed;
    
    private float _currentSpead;
    

    private void OnEnable()
    {
        
        HumanPlayer.SobrietyChanged += OnSobrietyChanged;
    }
    private void Start()
    {
        _currentSpead = _speed;
    }


    private void OnSobrietyChanged(SobrietyLevel sobriety)
    {
        switch (sobriety)
        {
            case SobrietyLevel.Sober:
                _currentSpead = _speed;
                break;
            case SobrietyLevel.Drunk:
                _currentSpead = _fastSpeed;

                break;
            case SobrietyLevel.DrunkAsHell:
                _currentSpead = _fastSpeed;
                break;
        }
    }



    void Update()
    {
        transform.Rotate(0f, 0f, _currentSpead * Time.deltaTime);
    }

    private void OnDisable()
    {
        
        HumanPlayer.SobrietyChanged -= OnSobrietyChanged;
    }
}
