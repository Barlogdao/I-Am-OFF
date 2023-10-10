using UnityEngine;
using YG;

public class AuthLeader : MonoBehaviour
{
    [SerializeField] GameObject _leaderBoard;
    [SerializeField] GameObject _authWindow;

    public void OpenLeaderBoard()
    {

        if (YandexGame.auth)
        {
            _leaderBoard.SetActive(true);
        }
        else
        {
            _authWindow.SetActive(true);
        }
    }

    public void RequestAuthorization()
    {
        YandexGame.AuthDialog();
        _authWindow.SetActive(false);
    }

}
