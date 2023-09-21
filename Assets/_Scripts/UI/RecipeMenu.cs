using UnityEngine;

public class RecipeMenu : MonoBehaviour
{
    [SerializeField] private RecipeComboVisual _recipeComboPrefab;

    private void Start()
    {
        foreach(var cocktail in DrinkProvider.Instance.GetAllRecipies())
        {
            Instantiate(_recipeComboPrefab, transform).Init(cocktail);
        }
    }

}
