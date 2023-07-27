using Assets.SimpleLocalization.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LevelLoader : MonoBehaviour
{

    
    private IEnumerator Start()
    {
        yield return null;
        LocalizationManager.Read();
        yield return new WaitUntil(() => YandexGame.SDKEnabled );
        LocalizationManager.Language = YandexGame.EnvironmentData.language;
        SceneManager.LoadScene(1);
    }
}
