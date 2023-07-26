using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class HumanPlayer : MonoBehaviour
{

    private int _drunkLevel;
    private int _score;
    private List<DrinkSO> _stomach = new();
    [SerializeField] private Canvas _offCanvas;
    public int Score => _score;
    private int _StaminaLevel;
    public SobrietyLevel Sobriety { get; private set; }
    public CircleCollider2D PlayerCollider { get; private set; }
    private Animator _animator;
    [SerializeField]
    private SpriteRenderer _drinkVisual;
    private int _drinkStrengtModifier;
    private int _secondStageLevel;
    private int _thirdStageLevel;
    public bool PlayerIsOFF { get; private set; }
    public bool PlayerIsDrinking { get; private set; }
    public int DrunkStrikeCount { get; private set; }

    public static event Action<float> DrunkLevelChanged;
    public static event Action<int> ScoreChanged;
    public static event Action<bool> PlayerMindChanged;
    public static event Action<SobrietyLevel> SobrietyChanged;
    public static event Action<List<DrinkSO>> StomachUpdated;
    public static event Action<CoctailRecipeSO> CoctailDrinked;
    public static event Action PlayerDrinked;
    public static event Action CoctailUnlocked;
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
        _StaminaLevel = data.Stamina;
        _secondStageLevel = data.SceondStageLevel;
        _thirdStageLevel = data.ThirdStageLevel;
        _drinkStrengtModifier = data.DrinkStrenghtModifier;
        _animator.runtimeAnimatorController = data.Animator;
        PlayerIsOFF = false;
        PlayerIsDrinking = false;
        SobrietyChanged?.Invoke(Sobriety);
        DrunkStrikeCount = 0;
        _offCanvas.gameObject.SetActive(false);
        Game.GameOvered += OnGameOvered;
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

    private void CoctailCheck()
    {
        var coctail = DrinkProvider.Instance.CheckCoctail(_stomach);
        if (coctail != null)
        {
            _score += coctail.BonusScore;
            ScoreChanged?.Invoke(_score);
            Game.Instance.GameTimer += coctail.BonusTime;
            CoctailDrinked?.Invoke(coctail);
            _stomach.Clear();
            StomachUpdated?.Invoke(_stomach);
            // If Coctail Not Unlocked
            //if (!SaveProvider.Instace.SaveData.RecipeIsUnlocked(coctail))
            //{
            //    SaveProvider.Instace.SaveData.UnlockRecipe(coctail);
            //    CoctailUnlocked?.Invoke();
            //}

        }
    }

    private void AddDrinkToStomach(DrinkSO drink)
    {
        if(_stomach.Count < 3)
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
        int drinkScore = 0;
        switch (drink.Rarity)
        {
            case DrinkRarity.Common:
                drinkScore = 5;
                break;
            case DrinkRarity.Uncommon:
                drinkScore = 10;
                break;
            case DrinkRarity.Rare:
                drinkScore = 15;
                break;
        }
        _score += drinkScore;
        ScoreChanged?.Invoke(_score);

    }

    private void DrunkResolve(DrinkSO drink)
    {
        _drunkLevel = Math.Clamp(_drunkLevel + drink.Strenght + _drinkStrengtModifier,0,_StaminaLevel);
        StartCoroutine(DrunkProcess());
       
    }

    private IEnumerator DrunkProcess()
    {
        DrinkAnimation();
        PlayerIsDrinking = true;
        yield return new WaitForSeconds(0.3f);
        PlayerIsDrinking = false;
        CoctailCheck();
        if (_drunkLevel >= _StaminaLevel)
        {
            _offCanvas.gameObject.SetActive(true);
            DrunkAnimation();
            PlayerIsOFF = true;
            PlayerMindChanged?.Invoke(PlayerIsOFF);
            DrunkStrikeCount += 1;
            yield return new WaitForSeconds(1f);
            _offCanvas.gameObject.SetActive(false);
            yield return new WaitForSeconds(Game.Instance.PlayerOFFTime - 1f);

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
