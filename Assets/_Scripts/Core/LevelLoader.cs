using Assets.SimpleLocalization.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] SaveProvider _saveProvider;
    [SerializeField] string _lang;

    private IEnumerator Start()
    {
        yield return null;
        LocalizationManager.Read();
        yield return new WaitUntil(() => YandexGame.SDKEnabled);

        //YandexGame.ResetSaveProgress();

        _saveProvider.Init();
#if UNITY_EDITOR
        LocalizationManager.Language = _lang;
#else
LocalizationManager.Language = YandexGame.EnvironmentData.language;
#endif
        SceneManager.LoadScene(1);
    }
}
