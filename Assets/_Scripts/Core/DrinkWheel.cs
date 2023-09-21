using UnityEngine;
using DG.Tweening;

public class DrinkWheel : MonoBehaviour
{

    [SerializeField] private GameConfig _gameConfig;
    
    private float _currentSpeed;
    

    private void OnEnable()
    {
        HumanPlayer.SobrietyChanged += OnSobrietyChanged;
    }
    private void Start()
    {
        _currentSpeed = _gameConfig.NormalSpeed;
    }


    private void OnSobrietyChanged(SobrietyLevel sobriety)
    {
        switch (sobriety)
        {
            case SobrietyLevel.Sober:
                
                SpeedTransition(_gameConfig.NormalSpeed);
                break;
            case SobrietyLevel.Drunk:
               SpeedTransition(_gameConfig.FastSpeed);

                break;
            case SobrietyLevel.DrunkAsHell:
                SpeedTransition(_gameConfig.SuperFastSpeed);
                break;
        }
    }
    private void SpeedTransition(float targetSpeed)
    {
        DOTween.To(() => _currentSpeed, x => _currentSpeed = x, targetSpeed, 0.9f);
    }


    void Update()
    {
        transform.Rotate(0f, 0f, _currentSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        HumanPlayer.SobrietyChanged -= OnSobrietyChanged;
    }
}
