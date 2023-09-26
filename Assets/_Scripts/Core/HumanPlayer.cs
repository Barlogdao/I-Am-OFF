using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class HumanPlayer : MonoBehaviour
{
    [SerializeField] private Canvas _offCanvas;
    [SerializeField] private SpriteRenderer _drinkVisual;
    [Header("score label")]
    [SerializeField] private TMPro.TextMeshProUGUI _scoreLabel;
    [SerializeField] float _scoreEndScale;
    private Vector3 _scoreStartScale;
    [SerializeField] float _scoreScaleDuration;

    [Header("drunk label")]
    [SerializeField] private TMPro.TextMeshProUGUI _drunkScoreLabel;
    [SerializeField] float _drunkEndScale;
    private Vector3 _drunkStartScale;
    [SerializeField] float _drunkScaleDuration;
    [SerializeField] private GameConfig _config;

    private List<DrinkSO> _stomach = new();

    private Animator _animator;
    private int _drunkLevel;
    private int _score;


    private int _StaminaLevel;
    private int _drinkStrengthModifier;
    private int _secondStageLevel;
    private int _thirdStageLevel;


    public int Score => _score;
    public SobrietyLevel Sobriety { get; private set; }
    public CircleCollider2D PlayerCollider { get; private set; }
    public bool PlayerIsOFF { get; private set; }
    public bool PlayerIsDrinking { get; private set; }
    public int EarnedCoins { get; private set; }

    public static event Action<float> DrunkLevelChanged;
    public static event Action<int> ScoreChanged;
    public static event Action<bool> PlayerMindChanged;
    public static event Action<SobrietyLevel> SobrietyChanged;
    public static event Action<List<DrinkSO>> StomachUpdated;
    public static event Action<CocktailRecipeSO> CoctailDrinked;
    public static event Action PlayerDrinked;
    private void Awake()
    {
        PlayerCollider = GetComponent<CircleCollider2D>();
    }

    public void Init(PlayerData data)
    {
        _animator = GetComponent<Animator>();
        Sobriety = SobrietyLevel.Sober;
        _score = 0;
        _drunkLevel = 0;
        SetPlayerCharacteristics(data,_config);

        _animator.runtimeAnimatorController = data.Animator;
        PlayerIsOFF = false;
        PlayerIsDrinking = false;
        SobrietyChanged?.Invoke(Sobriety);
        EarnedCoins = 0;
        _offCanvas.gameObject.SetActive(false);
        Game.GameOvered += OnGameOvered;

        _scoreStartScale = _scoreLabel.transform.localScale;
        _scoreLabel.enabled = false;
        _drunkStartScale = _drunkScoreLabel.transform.localScale;
        _drunkScoreLabel.enabled = false;
    }

    private void SetPlayerCharacteristics(PlayerData data, GameConfig config)
    {
        _StaminaLevel = data.GetStaminaLevel(config);
        _secondStageLevel = data.GetSecondStageLevel(config);
        _thirdStageLevel = data.GetThirdStageLevel(config);
        _drinkStrengthModifier = data.GetDrinkModifier(config);
    }

    private void OnGameOvered()
    {
        var sr = GetComponent<SpriteRenderer>();
        sr.sortingLayerName = "UI";
        sr.sortingOrder = 2;
    }

    public void Drink(DrinkSO drink)
    {
        PlayerDrinked?.Invoke();
        _drinkVisual.sprite = drink.Image;
        AddDrinkToStomach(drink);
        ScoreResolve(drink);
        DrunkResolve(drink);

    }

    private void CocktailCheck()
    {
        var cocktail = DrinkProvider.Instance.CheckCocktail(_stomach);
        if (cocktail != null)
        {
            _score += cocktail.BonusScore;
            ScoreChanged?.Invoke(_score);
            Game.Instance.AddBonusTime(cocktail.BonusTime);
            CoctailDrinked?.Invoke(cocktail);
            _stomach.Clear();
            StomachUpdated?.Invoke(_stomach);
        }
    }

    private void AddDrinkToStomach(DrinkSO drink)
    {
        if (_stomach.Count < 3)
        {
            _stomach.Add(drink);
        }
        else
        {
            _stomach.Clear();
            StomachUpdated?.Invoke(_stomach);
            _stomach.Add(drink);
        }
        StomachUpdated?.Invoke(_stomach);
    }

    private void ScoreResolve(DrinkSO drink)
    {
        var score = drink.GetDrinkScore();
        VisualizeDrinkScore(score);
        _score += score;
        ScoreChanged?.Invoke(_score);
    }

    private void VisualizeDrinkScore(int score)
    {
        _scoreLabel.enabled = true;
        _scoreLabel.text = $"+{score}";
        _scoreLabel.transform.DOScale(_scoreStartScale * _scoreEndScale, _scoreScaleDuration).OnComplete(() =>
        {
            _scoreLabel.transform.localScale = _scoreStartScale;
            _scoreLabel.enabled = false;
        });
    }

    private void DrunkResolve(DrinkSO drink)
    {
        var drunkAmount = drink.GetDrinkStrengthLevel() + _drinkStrengthModifier;
        _drunkLevel = Math.Clamp(_drunkLevel + drunkAmount, 0, _StaminaLevel);
        StartCoroutine(DrunkProcess(drunkAmount));
    }

    private void VisualizeDrunkLevel(int drunkAmount)
    {
        _drunkScoreLabel.enabled = true;
        _drunkScoreLabel.text = $"+{drunkAmount}";
        _drunkScoreLabel.transform.DOScale(_drunkStartScale * _drunkEndScale, _drunkScaleDuration).OnComplete(() =>
        {
            _drunkScoreLabel.transform.localScale = _drunkStartScale;
            _drunkScoreLabel.enabled = false;
        });
    }

    private IEnumerator DrunkProcess(int drunkAmount)
    {
        DrinkAnimation();
        PlayerIsDrinking = true;
        yield return new WaitForSeconds(0.3f);
        VisualizeDrunkLevel(drunkAmount);
        PlayerIsDrinking = false;
        CocktailCheck();
        if (_drunkLevel >= _StaminaLevel)
        {
            _offCanvas.gameObject.SetActive(true);
            DrunkAnimation();
            PlayerIsOFF = true;
            PlayerMindChanged?.Invoke(PlayerIsOFF);
            EarnedCoins += 1;
            yield return new WaitForSeconds(1f);
            _offCanvas.gameObject.SetActive(false);
            yield return new WaitForSeconds(Game.Instance.HangoverTime - 1f);

            _drunkLevel = 0;
            PlayerIsOFF = false;

            CheckSobriety();
            PlayerMindChanged?.Invoke(PlayerIsOFF);
            _animator.Play("Idle");
            yield break;
        }
        else
        {
            CheckSobriety();
        }


    }


    private void CheckSobriety()
    {
        SobrietyLevel level;

        if (_drunkLevel < _secondStageLevel)
        {
            level = SobrietyLevel.Sober;
        }
        else if (_drunkLevel < _thirdStageLevel)
        {
            level = SobrietyLevel.Drunk;
        }
        else
        {
            level = SobrietyLevel.DrunkAsHell;
        }

        if (Sobriety != level)
        {
            Sobriety = level;
            SobrietyChanged?.Invoke(Sobriety);
        }
        DrunkLevelChanged?.Invoke(GetDrunkLevel());
    }

    private float GetDrunkLevel()
    {
        if (_drunkLevel >= _StaminaLevel)
        {
            return 0f;
        }
        return (float)_drunkLevel / _StaminaLevel;
    }
    private void DrinkAnimation()
    {
        _animator.Play("Drink");
    }
    private void DrunkAnimation()
    {
        _animator.Play("OFF");
    }

    private void OnDestroy()
    {
        Game.GameOvered -= OnGameOvered;
        DOTween.KillAll();
    }


}

public enum SobrietyLevel
{
    Sober,
    Drunk,
    DrunkAsHell
}
