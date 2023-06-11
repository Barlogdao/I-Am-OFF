using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public bool AlkodzillaUnlocked;
    public bool SoberManUnlocked;

    public List<string> UnlockedRecipies;


    public SaveData()
    {
        UnlockedRecipies = new List<string>();
        SoberManUnlocked = false;
        AlkodzillaUnlocked = false;
    }

    public bool RecipeIsUnlocked(CoctailRecipeSO recipe)
    {
        return UnlockedRecipies.Contains(recipe.Name);
    }

    public void UnlockRecipe(CoctailRecipeSO recipe)
    {
        UnlockedRecipies.Add(recipe.Name);
    }
}
