using UnityEngine;
using UnityEngine.EventSystems;

public class ForRecordMusic : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private EventFloatSO _music;
    private bool _musicMuted = false;

    public void NoMusic(bool isMuted)
    {
        if (isMuted)
        {
            _music.RaiseEvent(0f);
        }
        else
        {
            _music.RaiseEvent(1f);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _musicMuted = !_musicMuted;
        NoMusic(_musicMuted);
    }
}
