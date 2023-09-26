using UnityEngine;
using UnityEngine.UI;
using YG;

public class MobileButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        if (YandexGame.EnvironmentData.isMobile)
        {
            var colorBlock = _button.colors;
            colorBlock.normalColor = Color.white;
            _button.colors = colorBlock;
        }
    }
}
