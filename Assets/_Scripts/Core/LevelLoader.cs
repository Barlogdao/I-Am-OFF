using Assets.SimpleLocalization.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] SaveProvider _saveProvider;
    
    private IEnumerator Start()
    {
        yield return null;
        LocalizationManager.Read();
        yield return new WaitUntil(() => YandexGame.SDKEnabled );
        _saveProvider.Init();
        LocalizationManager.Language = YandexGame.EnvironmentData.language;
        SceneManager.LoadScene(1);
    }
}
