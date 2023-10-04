using UnityEngine;

[CreateAssetMenu(fileName = "Playerdata", menuName = "_drinkData/PlayerData")]
public class PlayerData : CollectibleSO
{
    [field: SerializeField] public Sprite _visualImage { get; private set; }
    public string Name;
   
    public RuntimeAnimatorController Animator;
    public int DrinkStrenghtModifier;

    [Range(0f, 1f)]
    [SerializeField] public float StaminaValue;
    [Range(0f, 1f)]
    [SerializeField] public float SecondStageValue;
    [Range(0f, 1f)]
    [SerializeField] public float ThirdStageValue;
    [Range(0f, 1f)]
    [SerializeField] public float AlcoholResistanceValue;
    [Range(0f, 1f)]
    [SerializeField] public float HangoverResistanceValue;

    public int GetStaminaLevel(GameConfig config)
    {
        return (int)Mathf.Lerp(config.MinStamina, config.MaxStamina, StaminaValue);
    }

    public int GetSecondStageLevel(GameConfig config)
    {
        return (int) (GetStaminaLevel(config) * SecondStageValue);
    }

    public int GetThirdStageLevel(GameConfig config)
    {
        return (int)(GetStaminaLevel(config) * ThirdStageValue);
    }

    public int GetDrinkModifier(GameConfig config)
    {
        return (int)Mathf.Lerp(config.MaxDrunkModifier, config.MinDrunkModifier, AlcoholResistanceValue);
    }

    public float GetHangoverTime(GameConfig config)
    {
        return Mathf.Lerp(config.MaxHangoverTime, config.MinHangoverTime, HangoverResistanceValue);
    }
}
