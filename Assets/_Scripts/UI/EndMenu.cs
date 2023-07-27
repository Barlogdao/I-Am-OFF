using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    

    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }

}
