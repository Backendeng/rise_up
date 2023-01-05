using GoogleMobileAds.Api;
using UnityEngine;

public class RewardedVideoCallBack : MonoBehaviour {

    private void Start()
    {
        Timer.Schedule(this, 0.1f, AddEvents);
    }

    private void AddEvents()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (AdmobController.instance.rewardBasedVideo != null)
        {
            AdmobController.instance.rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        }
#endif
    }

    private const string ACTION_NAME = "rewarded_video";
    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        int amount = ConfigController.Config.rewardedVideoAmount;
        CurrencyController.CreditBalance(amount);
        Toast.instance.ShowMessage("You've received " + amount + " rubies", 3);
        CUtils.SetActionTime(ACTION_NAME);
    }

    private void OnDestroy()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (AdmobController.instance.rewardBasedVideo != null)
        {
            AdmobController.instance.rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        }
#endif
    }
}
