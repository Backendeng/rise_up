using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueFrame : MonoBehaviour {

    public Image progressBar;
    public Text timer;
    private int timeValue ;
    private bool timeRunning;
    private double startTime;
    public GameObject content;
    private bool rewardSuccess = false;

    void Start()
    {
        Timer.Schedule(this, 1, () =>
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        if (AdmobController.instance.rewardBasedVideo != null)
        {
            AdmobController.instance.rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            AdmobController.instance.rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        }
#endif
        });
    }

    public void HandleRewardBasedVideoClosed(object sender, System.EventArgs args)
    {
        if(!rewardSuccess)
            OnNoClick();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        CUtils.SetActionTime("show_ads");
        rewardSuccess = true;
        RewardSuccess();
    }

    private void RewardSuccess()
    {
        MainController.instance.ContinueLastGame();
    }

    public void OnShowRewardedClick()
    {
        timeRunning = false;
        content.SetActive(false);
        rewardSuccess = false;
        AdmobController.instance.ShowRewardBasedVideo();
        Sound.instance.PlayButton();
    }

    public void OnNoClick()
    {
        timeRunning = false;
        content.SetActive(false);
        MainController.instance.GameOver(false);
    }

    public void ShowContinueFrame()
    {
        content.SetActive(true);
        timeValue = 10;
        timeRunning = true;
        startTime = CUtils.GetCurrentTime();
        StartCoroutine(IERunCountDown());
    }

    void Update()
    {
        if(timeRunning)
        {
            float passTime = (float)(CUtils.GetCurrentTime() - startTime);
            progressBar.fillAmount = (1f - Mathf.Clamp01(passTime / 10));
        }
    }

    private IEnumerator IERunCountDown()
    {
        while (timeRunning)
        {
            UpdateText();
            yield return new WaitForSeconds(1);
            if (timeValue <= 0 || !timeRunning)
            {
                if(timeRunning)
                    OnNoClick();
                break;
            }
            else timeValue--;
        }
    }

    private void UpdateText()
    {
        timer.text = timeValue.ToString();
    }
}
