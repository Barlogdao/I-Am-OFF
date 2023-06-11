using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkProvider : MonoBehaviour
{
    public static DrinkProvider Instance { get; private set; } 

    private Dictionary<DrinkRarity, List<DrinkSO>> _drinkTable = new();
    private CoctailRecipeSO[] _coctailLibrary;

    [SerializeField] private int _uncommonDrinkChance;
    [SerializeField] private int _rareDrinkChance;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Drink.DrinkChanged += OnDrinkChanged;
        _coctailLibrary = Resources.LoadAll<CoctailRecipeSO>("Coctails");

        foreach (DrinkSO drink in Resources.LoadAll<DrinkSO>("Drinks"))
        {
            if (!_drinkTable.ContainsKey(drink.Rarity))
            {
                _drinkTable[drink.Rarity] = new List<DrinkSO>();
            }
            _drinkTable[drink.Rarity].Add(drink);
        }
    }

    private DrinkSO OnDrinkChanged()
    {
      
        int chance = UnityEngine.Random.Range(0, 100);
        if(chance <= _rareDrinkChance)
        {
            return GetDrink(DrinkRarity.Rare);
            
        }
        else if (chance <= _uncommonDrinkChance)
        {
            return GetDrink(DrinkRarity.Uncommon);
        }
        else
        {
            return GetDrink(DrinkRarity.Common);
        }
    }

    private DrinkSO GetDrink(DrinkRarity rarity)
    {
        return _drinkTable[rarity][UnityEngine.Random.Range(0, _drinkTable[rarity].Count)];
    }

    public CoctailRecipeSO CheckCoctail(List<DrinkSO> stomach)
    {
        foreach( var coctail in _coctailLibrary)
        {
            if (coctail.StomachHasRecipe(stomach))
            {
                return coctail;
            }
        }
        return null;
    }
    public CoctailRecipeSO[] GetAllRecipies()
    {
        return _coctailLibrary;
    }

    private void OnDestroy()
    {
        Drink.DrinkChanged -= OnDrinkChanged;
    }
}
