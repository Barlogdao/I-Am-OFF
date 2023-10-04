using UnityEngine;

public class StartAtPlay : MonoBehaviour
{
    [SerializeField] bool _isMenuScene;

    private void Start()
    {
        if (_isMenuScene)
        {
            AudioProvider.Instance.PlayMenuMusic();
        }
        else
        {
            AudioProvider.Instance.PlayCreditMusic();
        }
    }
}
