using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;
    private void Awake()
    {
        _playButton.onClick.AddListener(() => SceneManager.LoadScene(2));
        _exitButton.onClick.AddListener(() => Application.Quit());

    }
    private void Start()
    {
#if UNITY_WEBGL

     _exitButton.gameObject.SetActive(false);
#endif
    }

    public void Credits()
    {
        SceneManager.LoadScene(3);
    }
}
