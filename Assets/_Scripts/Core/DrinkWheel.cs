using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class DrinkWheel : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Drink _drinkPrefab;

    private float _currentSpeed;
    private Transform _transform;
    private SobrietyLevel _currentSobriety;
    private float _timer = 0f;
    [SerializeField] private LayerMask _drinkLayerMask;
    private Game _game;

    public void Init(Game game)
    {
        _game = game;
        _currentSpeed = _gameConfig.NormalSpeed;
        _transform = transform;
        _currentSobriety = SobrietyLevel.Sober;
        CreateDrinks();
    }

    private void OnEnable()
    {
        HumanPlayer.SobrietyChanged += OnSobrietyChanged;
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

    private void Update()
    {
        _timer += Time.deltaTime;
        MoveWheel();
    }

    private void CreateDrinks()
    {
        var angle = 360;
        var count = _gameConfig.DrinksAmount;
        var distance = _gameConfig.Distance;
        var spawnForm = GetSpawnForm();
        var offset = 0.1f;
        int rows = _gameConfig.DrinkRows;

        if (spawnForm == SpawnForm.roundEqual)
        {
            for (int i = 0; i < rows; i++)
            {
                SetOnAngle(distance + (1.2f * i), angle, count / rows, _drinkPrefab, 0);
                _transform.Rotate(0f, 0f, 30f);
            }
        }

        else if (spawnForm == SpawnForm.roundProgressive)
        {
            int step = (count / GetProportion(rows)) + 1;

            for (int i = 0; i < rows; i++)
            {
                SetOnAngle(distance + (1.2f * i), angle, step * (i + 1), _drinkPrefab, 0);
                _transform.Rotate(0f, 0f, 45f);
            }
        }
        else if (spawnForm == SpawnForm.spiral)
        {
            for (int i = 0; i < rows; i++)
            {
                SetOnAngle(distance + (1.1f * i), angle, count / rows, _drinkPrefab, offset);
                _transform.Rotate(0f, 0f, 20f);
            }
        }
        else if (spawnForm == SpawnForm.rows)
        {
            for (int i = 0; i < 4; i++)
            {
                SetRow(distance + (0.3f * i), angle, count / 4, _drinkPrefab, offset);
                _transform.Rotate(0f, 0f, 90f);
            }
        }
    }

    private SpawnForm GetSpawnForm()
    {
        switch (Random.Range(0, 4))
        {
            case 0: return SpawnForm.spiral;

            case 1: return SpawnForm.rows;

            case 2: return SpawnForm.roundEqual;

            case 3: return SpawnForm.roundProgressive;

            default: return SpawnForm.spiral;
        }
    }

    private void SetRow(float distance, float angle, int count, Drink prefab, float offset)
    {
        Vector3 point = _transform.position;
        for (int i = 0; i < count; i++)
        {
            float x = _transform.position.x - count + (i * 2f);
            float y = distance + Mathf.Sin(i * 0.2f);
            point.x = x;
            point.y = y;

            SpawnDrink(prefab, point);
        }
    }

    private void SetOnAngle(float distance, float angle, int count, Drink prefab, float offset)
    {
        Vector3 point = _transform.position;
        angle *= Mathf.Deg2Rad;
        for (int i = 1; i <= count; i++)
        {
            float x = _transform.position.x + (Mathf.Sin(angle / count * i) * distance) + offset * i;
            float y = _transform.position.y + (Mathf.Cos(angle / count * i) * distance) + offset * i;
            point.x = x;
            point.y = y;

            if (Random.value > 0.9f)
            {
                continue;
            }

            SpawnDrink(prefab, point);
        }
    }

    private void MoveWheel()
    {
        switch (_currentSobriety)
        {
            case SobrietyLevel.Sober:
                _transform.Rotate(0f, 0f, _currentSpeed * Time.deltaTime);
                _transform.position = Vector2.MoveTowards(_transform.position, Vector2.zero, Time.deltaTime);
                break;

            case SobrietyLevel.Drunk:
                _transform.Rotate(0f, 0f, _currentSpeed * Time.deltaTime);
                _transform.position = Vector2.MoveTowards(_transform.position, new Vector2(MathF.Sin(Time.time), MathF.Cos(Time.time)), Time.deltaTime);
                break;

            case SobrietyLevel.DrunkAsHell:
                _transform.Rotate(0f, 0f, (_currentSpeed + Mathf.Sign(_currentSpeed) * _timer * 2) * Time.deltaTime);
                _transform.position = Vector2.MoveTowards(_transform.position, new Vector2(MathF.Sin(Time.time), MathF.Cos(Time.time)) * 1.5f, Time.deltaTime);
                break;
        }
    }

    private void SpawnDrink(Drink prefab, Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity, _transform).Init(_game, _transform);
    }

    private void OnDisable()
    {
        HumanPlayer.SobrietyChanged -= OnSobrietyChanged;
    }

    private int GetProportion(int count)
    {
        int temp = 0;
        for (int i = 1; i <= count; i++)
        {
            temp += i;
        }

        return temp;
    }
}
