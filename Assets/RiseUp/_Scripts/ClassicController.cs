using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Superpow;

public class ClassicController : MonoBehaviour {

    public GameObject gameOverTitle, scoreObj, levelObj;
    public Text scoreText, levelText, gameOverScoreText, bestScoreText;
    private int score, currLevel;
    private double lastTimeScore;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            scoreText.text = score.ToString();
        }
    }

    public void UpdateLevel(int currentLevel)
    {
        currLevel = currentLevel;
        levelText.text = currentLevel == 0 ? "1" : currentLevel.ToString();
    }

    public void InitClassic()
    {
        Score = 0;
        currLevel = 0;
        lastTimeScore = CUtils.GetCurrentTime();
    }

    public void ResetTimeScore()
    {
        lastTimeScore = CUtils.GetCurrentTime();
    }

    public void ShowLevelAndScore(bool isShow)
    {
        scoreObj.SetActive(isShow);
        levelObj.SetActive(isShow);
    }

    public void ShowGameOver(bool isShow)
    {
        gameOverTitle.SetActive(isShow);
        gameOverScoreText.text = score.ToString();
        if (isShow)
        {
            int best = Utils.GetBestScore();
            if (best < score)
            {
                Utils.SetBestScore(score);
            }
        }
        bestScoreText.text = Utils.GetBestScore().ToString();
    }

    private void FixedUpdate()
    {
        if (MainController.IsPlaying())
        {
            if (currLevel >= 1 && CUtils.GetCurrentTime() - lastTimeScore >= 1)
            {
                Score++;
                lastTimeScore = CUtils.GetCurrentTime();
            }
        }
    }
}
