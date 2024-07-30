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
        //yield return new WaitUntil(() => YandexGame.SDKEnabled);
        _saveProvider.Init();


        LocalizationManager.Language = GetPlayerLanguage();

        SceneManager.LoadScene(1);
    }
    private string GetPlayerLanguage()
    {
        string lang;

        switch (Application.systemLanguage)
        {
            case SystemLanguage.English:
                lang = "en";
                break;

            case SystemLanguage.French:
                lang = "fr";
                break;

            case SystemLanguage.German:
                lang = "de";
                break;

            case SystemLanguage.Russian:
                lang = "ru";
                break;

            case SystemLanguage.Turkish:
                lang = "tr";
                break;

            default:
                lang = "en";
                break;
        }

        return lang;
    }
}

