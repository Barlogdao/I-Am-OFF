using System;
using UnityEngine;
using YG;

public class RewardProvider : MonoBehaviour
{
    public static Action<Action> RewardRequested;
    private Action _unlockAction;

    private void OnEnable()
    {
        RewardRequested += OnRewardRequested;
        //YandexGame.RewardVideoEvent += Rewarded;
    }

    private void Rewarded(int number)
    {
        _unlockAction.Invoke();
        _unlockAction = null;
    }

    private void OnRewardRequested(Action action)
    {
        _unlockAction = action;
        //YandexGame.RewVideoShow(0);
    }

    private void OnDisable()
    {
        RewardRequested -= OnRewardRequested;
        //YandexGame.RewardVideoEvent -= Rewarded;
    }
}
