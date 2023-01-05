using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Superpow;

public class ChallengeController : MonoBehaviour {

    public GameObject challengeGuide, homeBtn, levelProgress, levelCompleteFrame;
    [HideInInspector]
    public List<int> listObstacles;
    public Image progressMask;
    public Text currLevelChallengeText, nextLevelChallengeText;
    private float lastBackStartPosY;
    private BackItem curLastBack;
    private float passedDelta = 0;
    public Image status;
    public Sprite completedSprite, failedSprite;
    public Button nextBtn, replayBtn;

    public void UpdateNewLevel(BackItem lastBack, float delta)
    {
        passedDelta = delta;
        curLastBack = lastBack;
        lastBackStartPosY = curLastBack.rect.anchoredPosition.y;
    }

    public void SetPassDelta(float delta)
    {
        curLastBack = null;
        passedDelta = delta;
        float percent = Mathf.Clamp(passedDelta / (listObstacles.Count * Const.BACK_HEIGHT * 2), 0, 1);
        progressMask.rectTransform.sizeDelta = new Vector2(percent * 400, 100);
    }

    public void ShowLevelComplete(bool isCompleted)
    {
        Sound.instance.Play(isCompleted ? Sound.Others.Complete : Sound.Others.Failed);
        status.sprite = isCompleted ? completedSprite : failedSprite;
        status.SetNativeSize();
        nextBtn.gameObject.SetActive(isCompleted);
        replayBtn.gameObject.SetActive(!isCompleted);
        if (isCompleted)
        {
            int nextLevel = Mathf.Min(Utils.GetChallengeLevel() + 1, Const.Levels.Length);
            Utils.SetChallengeLevel(nextLevel);
        }
    }

    public void LoadChallenge()
    {
        passedDelta = 0;
        progressMask.rectTransform.sizeDelta = new Vector2(0, 100);
        int challengeLevel = Utils.GetChallengeLevel();
        currLevelChallengeText.text = challengeLevel.ToString();
        nextLevelChallengeText.text = (challengeLevel + 1).ToString();
        listObstacles = ParseListObstacles(Const.Levels[challengeLevel - 1]);
    }

    public void ShowHomeBtn(bool isShow)
    {
        homeBtn.SetActive(isShow);
    }

    public void ShowDialog(bool isShow)
    {
        levelCompleteFrame.SetActive(isShow);
    }

    public void ShowGuide(bool isShow)
    {
        challengeGuide.SetActive(isShow);
    }

    public void ShowProgressLevel(bool isShow)
    {
        levelProgress.SetActive(isShow);
    }

    public List<int> ParseListObstacles(string list)
    {
        List<int> obs = new List<int>();
        string[] arr = list.Split(' ');
        foreach (string num in arr)
        {
            int result;
            bool success = int.TryParse(num, out result);
            if (success) obs.Add(result);
        }
        return obs;
    }

    public void ReplayClick()
    {
        MainController.instance.ResetGame();
        MainController.instance.LoadChallenge();
    }

    public void NextClick()
    {
        MainController.instance.ResetGame();
        MainController.instance.LoadChallenge();
    }

    public void Update()
    {
        if (MainController.IsPlaying() && !MainController.IsClassicMode() && curLastBack != null)
        {
            float delta = passedDelta + lastBackStartPosY - curLastBack.rect.anchoredPosition.y;
            float percent = Mathf.Clamp(delta / (listObstacles.Count * Const.BACK_HEIGHT * 2), 0, 1);
            progressMask.rectTransform.sizeDelta = new Vector2(percent * 400, 100);
        }
    }
}
