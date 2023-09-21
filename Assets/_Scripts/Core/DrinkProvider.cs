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
        if (chance <= _rareDrinkChance)
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

    public CocktailRecipeSO CheckCocktail(List<DrinkSO> stomach)
    {
        foreach (var cocktail in _cocktailSheet.CocktailRecipes)
        {
            if (cocktail.StomachHasRecipe(stomach) && SaveProvider.Instace.SaveData.IsRecipeUnlocked(cocktail.ID))
            {
                return cocktail;
            }
        }

        return null;
    }
    public CocktailRecipeSO[] GetAllRecipies()
    {
        return _cocktailSheet.CocktailRecipes
            .OrderBy(x => x.ID)
            .ToArray();
    }

    private void OnDestroy()
    {
        Drink.DrinkChanged -= OnDrinkChanged;
    }
}
