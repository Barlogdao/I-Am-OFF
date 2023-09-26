using UnityEngine;

public class FirstTimeHideElement : MonoBehaviour
{
    private void Start()
    {
        if (SaveProvider.Instace.SaveData.MaxScore == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
