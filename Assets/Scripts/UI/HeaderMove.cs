using UnityEngine;
using DG.Tweening;

public class HeaderMove : MonoBehaviour
{
    [SerializeField] float _size, _loopTime;
    private void Start()
    {
        transform.DOScale(_size, _loopTime).SetEase(Ease.InOutQuad).SetLoops(-1,LoopType.Yoyo);
    }
    private void OnDestroy()
    {
        DOTween.KillAll();
    }

}
