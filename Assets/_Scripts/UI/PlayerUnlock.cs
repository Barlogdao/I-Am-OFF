using UnityEngine;

public class PlayerUnlock : MonoBehaviour
{
    void Start()
    {
        if (SaveProvider.Instace.SaveData.UnlockedBackgrounds.Count < 2)
        {
            gameObject.SetActive(false);
        }
    }
}
