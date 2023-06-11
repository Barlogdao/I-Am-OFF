using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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
        _animator.runtimeAnimatorController = data.Animator;
        PlayerIsOFF = false;
        PlayerIsDrinking = false;
        SobrietyChanged?.Invoke(Sobriety);
        DrunkStrikeCount = 0;
        _offCanvas.gameObject.SetActive(false);
    }

    public void Drink(DrinkSO drink)
    {
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
            CoctailDrinked?.Invoke(coctail);
            _stomach.Clear();
            StomachUpdated?.Invoke(_stomach);
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
        _score += drink.Reward;
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

    
}

public enum SobrietyLevel
{
    Sober,
    Drunk,
    DrunkAsHell
}
