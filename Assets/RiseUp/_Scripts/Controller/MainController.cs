using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Superpow;

public class MainController : BaseController {
    
    public Transform backRegion, spawnRegion;
    public BackItem backPrefab;
    public List<GroupObstacle> obstaclePrefabs;
    private int colorIndex = 0, backCount = 0;
    private BackItem lastBack = null;
    public enum GameState {START, LOADED, PLAYING, GAME_OVER, COMPLETED, GUIDE, SHOP, PAUSED};
    public static GameState gameState = GameState.START, lastState = GameState.START;
    public GameObject startFrame, guideFrame, shopFrame, gameTitle;
    public Protection protection;
    public ChallengeController challengeController;
    public ClassicController classicController;
    public static MainController instance;
    public ContinueFrame continueFrame;
    public MyPlayer player;
    private int spawnLevel = 1, passedLevel = 0, currentLevel = 0;
    private int savedObstacleIndex, lastObstacleIndex;

    public Animator flash;
    
    private List<BackItem> backs = new List<BackItem>();

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    protected override void Start()
    {
        base.Start();
        SetupBackground();
        LoadAllObstacles();
    }

    private void LoadAllObstacles()
    {
        obstaclePrefabs = Resources.LoadAll<GroupObstacle>("GroupObstacles").ToList();
        obstaclePrefabs.Sort((x, y) => Utils.GetGroupObIndex(x.name).CompareTo(Utils.GetGroupObIndex(y.name)));
    }

    private void SetupBackground()
    {
        lastBack = SpawnNewBackground(Vector3.zero);        
        lastBack.SetUp(0, 5, true, true, 0);
        backs.Add(lastBack);
        colorIndex = Random.Range(1, 5);
        float canvasHeight = backRegion.parent.GetComponent<RectTransform>().sizeDelta.y;
        for (int i = 0; i < 2; i++)
        {
            Vector3 pos = new Vector3(0, lastBack.rect.anchoredPosition.y + (i == 0 ? canvasHeight : Const.BACK_HEIGHT));
            lastBack = SpawnNewBackground(pos);
            lastBack.SetUp(colorIndex, colorIndex, i == 0, false, 1);
            backs.Add(lastBack);            
        }
    }

    public void OnNewLevel()
    {
        currentLevel++;
        if (IsClassicMode())
        {
            classicController.UpdateLevel(currentLevel);
        }
        else
        {
            challengeController.UpdateNewLevel(lastBack, (currentLevel - 1) * Const.BACK_HEIGHT * 2);
        }
        savedObstacleIndex = lastObstacleIndex;
    }

    public void OnPassLevel()
    {
        passedLevel++;
        if(!IsClassicMode() && passedLevel == challengeController.listObstacles.Count)
        {
            //level completed
            SetState(GameState.COMPLETED);
            challengeController.ShowLevelComplete(true);
            UpdateUI();
        }
    }

    public static bool IsClassicMode()
    {
        return Utils.GetGameMode() == Utils.CLASSIC_MODE;
    }

    private BackItem SpawnNewBackground(Vector3 localPos)
    {
        BackItem back = (BackItem)Instantiate(backPrefab, Vector3.zero, Quaternion.identity);
        back.transform.SetParent(backRegion);
        back.GetComponent<RectTransform>().anchoredPosition3D = localPos;
        back.transform.localScale = Vector3.one;
        back.onNewLevel += OnNewLevel;
        back.onPassLevel += OnPassLevel;
        return back;
    }

    public void MoveBackgroundToTop(BackItem back)
    {
        back.rect.anchoredPosition = new Vector3(0, lastBack.rect.anchoredPosition.y + Const.BACK_HEIGHT - 10);
        lastBack = back;
        int lastIndex = colorIndex;
        if(backCount == 0)
        {
            spawnLevel++;
            if (IsClassicMode())
            {
                colorIndex = (colorIndex + 1) % 5;
                SpawnNewObstacle(Random.Range(0, obstaclePrefabs.Count), back.rect.anchoredPosition / 100);
            }
            else
            {
                if (spawnLevel <= challengeController.listObstacles.Count)
                {
                    colorIndex = (colorIndex + 1) % 5;
                    SpawnNewObstacle(challengeController.listObstacles[spawnLevel - 1] - 1, back.rect.anchoredPosition / 100);
                }
            }
        }
        backCount = (backCount + 1) % 2;
        lastBack.SetUp(colorIndex, colorIndex != lastIndex ? lastIndex : colorIndex, colorIndex != lastIndex, false, spawnLevel);
    }

    public void LoadAndStartClassic()
    {
        Sound.instance.PlayButton();
        currentLevel = 0;
        classicController.InitClassic();
        SpawnNewObstacle(Random.Range(0, obstaclePrefabs.Count), backs[1].rect.anchoredPosition / 100);
        Utils.SetGameMode(Utils.CLASSIC_MODE);
        StartGame();
    }

    public void UpdateUI()
    {
        startFrame.SetActive(gameState == GameState.START || (gameState == GameState.GAME_OVER && IsClassicMode()));
        protection.gameObject.SetActive(IsPlaying() || gameState == GameState.LOADED);
        player.gameObject.SetActive(gameState != GameState.GUIDE && gameState != GameState.SHOP);

        guideFrame.SetActive(gameState == GameState.GUIDE);
        shopFrame.SetActive(gameState == GameState.SHOP);
        gameTitle.SetActive(gameState == GameState.START);

        classicController.ShowGameOver(IsClassicMode() && gameState == GameState.GAME_OVER);
        classicController.ShowLevelAndScore(IsClassicMode() && (IsPlaying() || gameState == GameState.GAME_OVER));

        challengeController.ShowHomeBtn(!IsClassicMode() && !IsPlaying() && gameState != GameState.GUIDE && gameState != GameState.SHOP);
        challengeController.ShowProgressLevel(!IsClassicMode() && (IsPlaying() || gameState == GameState.LOADED || gameState == GameState.GAME_OVER));
        challengeController.ShowGuide(!IsClassicMode() && gameState == GameState.LOADED);
        challengeController.ShowDialog(!IsClassicMode() && (gameState == GameState.COMPLETED || gameState == GameState.GAME_OVER));
    }

    public void LoadChallenge()
    {
        Sound.instance.PlayButton();
        currentLevel = 0;
        challengeController.LoadChallenge();
        SpawnNewObstacle(challengeController.listObstacles[0] - 1, backs[1].rect.anchoredPosition / 100);
        Utils.SetGameMode(Utils.CHALLENGE_MODE);
        SetState(GameState.LOADED);
        UpdateUI();
    }

    public void StartGame()
    {
        SetState(GameState.PLAYING);
        UpdateUI();
    }

    public static bool IsPlaying()
    {
        return gameState == GameState.PLAYING;
    }

    public static bool IsLoaded()
    {
        return gameState == GameState.LOADED;
    }

    private void SpawnNewObstacle(int index, Vector3 localPos)
    {
        lastObstacleIndex = index;
        GroupObstacle obstacle = Instantiate(obstaclePrefabs[index], Vector3.zero, Quaternion.identity);
        obstacle.transform.SetParent(spawnRegion);
        obstacle.transform.localPosition = localPos;
        obstacle.transform.localScale = Vector3.one;
    }


    public void ShowGuide()
    {
        Sound.instance.PlayButton();
        SetState(GameState.GUIDE);
        UpdateUI();
    }

    public void ShowShop()
    {
        Sound.instance.PlayButton();
        SetState(GameState.SHOP);
        UpdateUI();
    }

    public void SetState(GameState newState)
    {
        lastState = gameState;
        gameState = newState;
    }

    public void HomeBtnClick(bool reset)
    {
        Sound.instance.PlayButton();
        if (reset)
        {
            ResetGame();
            Utils.SetGameMode(Utils.CLASSIC_MODE);
            SetState(GameState.START);
        }
        else
        {
            GameState state = lastState;
            SetState(state);
        }
        UpdateUI();
    }

    public void RateGameClick()
    {
        CUtils.RateGame();
    }

    public void ShowLeaderboard()
    {

    }

    public void CheckAndShowContinue()
    {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        if (AdmobController.instance.rewardBasedVideo.IsLoaded() && currentLevel >= 2)
        {
            gameState = GameState.PAUSED;
            continueFrame.ShowContinueFrame();
        }
        else
            GameOver();
#else
        GameOver();
#endif
    }

    public void CollideWithObstacle()
    {
        SlowObstacle();
        flash.SetTrigger("show");
    }

    public void GameOver(bool delay = true)
    {
        SetState(GameState.GAME_OVER);
        Timer.Schedule(this, delay ? 1.5f : 0f, () =>
        {
            CUtils.ShowInterstitialAd();
            if (IsClassicMode())
            {
                Sound.instance.Play(Sound.Others.Failed);
                ResetGame();
            }
            else
            {
                challengeController.ShowLevelComplete(false);
            }
            UpdateUI();
        });
    }

    public void ResetGame()
    {
        ClearObstacle();
        protection.transform.localPosition = Vector3.zero;
        ResetBackground(1);
        player.Reset();
        protection.RemoveVelocity();
        spawnLevel = 1;
        passedLevel = 0;
    }

    public void ContinueLastGame()
    {
        ClearObstacle();
        protection.transform.localPosition = Vector3.zero;
        ResetBackground(currentLevel);
        player.Reset();
        protection.RemoveVelocity();
        if (IsClassicMode())
        {
            classicController.ResetTimeScore();
        }
        else
        {
            challengeController.SetPassDelta((currentLevel - 1) * Const.BACK_HEIGHT * 2);
        }
        SpawnNewObstacle(savedObstacleIndex, backs[1].rect.anchoredPosition / 100);
        currentLevel = currentLevel - 1;
        passedLevel = currentLevel;
        spawnLevel = currentLevel + 1;
        StartGame();
    }

    private void SlowObstacle()
    {
        List<Transform> children = CUtils.GetChildren(spawnRegion);
        foreach (Transform child in children)
        {
            if(child.tag =="Obstacle")
            {
                child.GetComponent<Rigidbody2D>().drag = child.GetComponent<Rigidbody2D>().drag * 10;
                child.GetComponent<Rigidbody2D>().angularDrag = child.GetComponent<Rigidbody2D>().angularDrag * 10;
            }
        }
    }

    private void ClearObstacle()
    {
        List<Transform> children = CUtils.GetChildren(spawnRegion);
        foreach(Transform child in children)
        {
            Destroy(child.gameObject);
        }
    }

    private void ResetBackground(int startLevel)
    {
        float canvasHeight = backRegion.parent.GetComponent<RectTransform>().sizeDelta.y;
        backs[0].rect.anchoredPosition3D = Vector3.zero;
        backs[0].SetUp(0, 5, true, true, 0);
        colorIndex = Random.Range(1, 5);
        for (int i = 1; i < 3; i++)
        {
            Vector3 pos = new Vector3(0, backs[i - 1].rect.anchoredPosition.y + (i == 1 ? canvasHeight : Const.BACK_HEIGHT));
            backs[i].rect.anchoredPosition3D = pos;
            backs[i].SetUp(colorIndex, colorIndex, i == 1, false, startLevel);            
            backs[i].transform.SetAsLastSibling();
            lastBack = backs[i];
        }
        backCount = 0;        
    }

    double lastTime = 0;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            double time = CUtils.GetCurrentTimeInMills();
            if (time - lastTime < 3000)
            {
                Application.Quit();
            }
            else
            {
                Toast.instance.ShowMessage("Press back again to quit");
                lastTime = time;
            }
        }
    }
}
