using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

public class HumanPlayer : MonoBehaviour
{

    private int _drunkLevel;
    private int _score;
    [SerializeField] private int _StaminaLevel;
    public SobrietyLevel Sobriety { get; private set; }
    public CircleCollider2D PlayerCollider { get; private set; }

    public bool PlayerIsOFF { get; private set; }

    public static event Action<float> DrunkLevelChanged;
    public static event Action<int> ScoreChanged;
    public static event Action<bool> PlayerMindChanged;
    public static event Action<SobrietyLevel> SobrietyChanged;
    private void Awake()
    {
        PlayerCollider = GetComponent<CircleCollider2D>();
    }

    public void Init()
    {

        Sobriety = SobrietyLevel.Sober;
        _score = 0;
        _drunkLevel = 0;
        PlayerIsOFF = false;
    }

    public void Drink(DrinkSO drink)
    {
        ScoreResolve(drink);
        DrunkResolve(drink);
    }
    private void ScoreResolve(DrinkSO drink)
    {
        _score += drink.Reward;
        ScoreChanged?.Invoke(_score);
    }

    private void DrunkResolve(DrinkSO drink)
    {
        _drunkLevel = Math.Clamp(_drunkLevel + drink.Strenght,0,_StaminaLevel);
        StartCoroutine(DrunkProcess());
       
    }

    private IEnumerator DrunkProcess()
    {
        yield return null;
        if (_drunkLevel >= _StaminaLevel)
        {
            PlayerIsOFF = true;
            PlayerMindChanged?.Invoke(PlayerIsOFF);
            yield return new WaitForSeconds(1f);
            while (_drunkLevel > 0)
            {
                _drunkLevel = Math.Clamp(_drunkLevel - _StaminaLevel/(int)(Game.Instance.PlayerOFFTime -1), 0, _StaminaLevel);
                CheckSobriety();
                yield return new WaitForSeconds(1f);
            }
            PlayerIsOFF = false;
            PlayerMindChanged?.Invoke(PlayerIsOFF);
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

        if (_drunkLevel < _StaminaLevel * 0.33f)
        {
            level = SobrietyLevel.Sober;
        }
        else if (_drunkLevel < _StaminaLevel * 0.66f)
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

    
}

public enum SobrietyLevel
{
    Sober,
    Drunk,
    DrunkAsHell
}
