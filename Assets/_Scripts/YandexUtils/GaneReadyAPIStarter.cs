using UnityEngine;
using YG;

public class GaneReadyAPIStarter : MonoBehaviour
{
    private static bool _apiStarted=false;
    void Start()
    {
        if (_apiStarted == false)
        {
            YandexGame.GameReadyAPI();
            _apiStarted = true;
        }
    }


}
