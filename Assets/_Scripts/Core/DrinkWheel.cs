using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class DrinkWheel : MonoBehaviour
{

    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Drink _drinkPrefab;

    private float _currentSpeed;
    private Transform _transform;
    private SobrietyLevel _currentSobriety;
    private float _timer = 0f;

    private void OnEnable()
    {
        HumanPlayer.SobrietyChanged += OnSobrietyChanged;
    }
    private void Start()
    {
        _currentSpeed = _gameConfig.NormalSpeed;
        _transform = transform;
        _currentSobriety = SobrietyLevel.Sober;
        CreateDrinks();
    }

    private void OnSobrietyChanged(SobrietyLevel sobriety)
    {
        _currentSobriety = sobriety;
        _timer = 0f;
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
        _timer += Time.deltaTime;
        MoveWheel();
    }

    private void CreateDrinks()
    {
        var angle = 360;
        var count = _gameConfig.DrinksAmount;
        var distance = 3f;
        var offset = 0.1f;
        int rows = _gameConfig.DrinkRows;
        float middleAngle = angle / rows;

        for (int i = 0; i < rows; i++)
        {
            SetOnAngle(distance + (i * 1.2f), angle, count / rows, _drinkPrefab.gameObject, offset);
            _transform.Rotate(0f, 0f, middleAngle);
        }
    }

    private void SetOnAngle(float distance, float angle, int count, GameObject prefab, float offset)
    {
        Vector3 point = _transform.position;
        angle *= Mathf.Deg2Rad;
        for (int i = 1; i <= count; i++)
        {
            float x = _transform.position.x + (Mathf.Sin(angle / count * i) * distance);
            float y = _transform.position.y + (Mathf.Cos(angle / count * i) * distance);
            point.x = x;
            point.y = y;
            if (Random.value > 0.85f)
                continue;
            Instantiate(prefab, point, Quaternion.identity, _transform);
        }
    }

    private void MoveWheel()
    {
        //_transform.Rotate(0f, 0f, _currentSpeed * Time.deltaTime);
        switch (_currentSobriety)
        {
            case SobrietyLevel.Sober:
                _transform.Rotate(0f, 0f, _currentSpeed * Time.deltaTime);
                _transform.position = Vector2.MoveTowards(_transform.position, Vector2.zero, 1f * Time.deltaTime);
                break;
            case SobrietyLevel.Drunk:
                _transform.Rotate(0f, 0f, _currentSpeed * Time.deltaTime);
                _transform.position = Vector2.MoveTowards(_transform.position, new Vector2(MathF.Sin(Time.time), MathF.Cos(Time.time)), 1f * Time.deltaTime);
                break;
            case SobrietyLevel.DrunkAsHell:
                _transform.Rotate(0f, 0f, (_currentSpeed + Mathf.Sign(_currentSpeed) * _timer * 2) * Time.deltaTime);
                _transform.position = Vector2.MoveTowards(_transform.position, new Vector2(MathF.Sin(Time.time), MathF.Cos(Time.time)) * 2, 1f * Time.deltaTime);
                break;
        }
    }

    private void OnDisable()
    {
        HumanPlayer.SobrietyChanged -= OnSobrietyChanged;
    }
}
