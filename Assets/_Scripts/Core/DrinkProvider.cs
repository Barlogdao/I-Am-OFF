using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrinkProvider : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] DrinkSheet _drinkSheet;
    [SerializeField] CocktailSheet _cocktailSheet;
    public static DrinkProvider Instance { get; private set; } 

    private Dictionary<DrinkRarity, List<DrinkSO>> _drinkTable = new();
    private CoctailRecipeSO[] _cocktailLibrary;

    private int _uncommonDrinkChance;
    private int _rareDrinkChance;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _uncommonDrinkChance = _gameConfig.UncommonDrinkAppearanceChance;
        _rareDrinkChance = _gameConfig.RareDrinkAppearanceChance;

        Drink.DrinkChanged += OnDrinkChanged;
        _cocktailLibrary = _cocktailSheet.CocktailRecipes;

        foreach (DrinkSO drink in _drinkSheet.Drinks)
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

    public CoctailRecipeSO CheckCocktail(List<DrinkSO> stomach)
    {
        foreach( var coctail in _cocktailLibrary)
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
        return _cocktailLibrary.OrderBy(x => x.CoinCost).ThenBy(y => !SaveProvider.Instace.SaveData.RecipeIsUnlocked(y)).ToArray(); 
    }

    private void OnDestroy()
    {
        Drink.DrinkChanged -= OnDrinkChanged;
    }
}
