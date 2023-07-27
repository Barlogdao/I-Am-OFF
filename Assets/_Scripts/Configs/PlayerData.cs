using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Playerdata", menuName = "_drinkData/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string Name;
    public int Stamina;
    public int SceondStageLevel;
    public int ThirdStageLevel;
    public float OFFTime;
    public RuntimeAnimatorController Animator;
    public int DrinkStrenghtModifier;

}
