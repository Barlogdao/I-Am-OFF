using UnityEngine;
using YG;

public class StickyBannerActivator : MonoBehaviour
{
    [SerializeField] private bool isStickyActive;
    [SerializeField] bool _ignoreIfDesktop;
    private void Start()
    {
        if(_ignoreIfDesktop == true && YandexGame.EnvironmentData.isDesktop)
        {
            return;
        }

        YandexGame.StickyAdActivity(isStickyActive);
    }
}
