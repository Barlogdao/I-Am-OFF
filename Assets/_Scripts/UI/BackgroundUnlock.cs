using UnityEngine;

public class BackgroundUnlock : MonoBehaviour
{
    void Start()
    {
        if(SaveProvider.Instace.SaveData.UnlockedRecipes.Count < 2)
        {
            gameObject.SetActive(false); 
        }
    }
}
