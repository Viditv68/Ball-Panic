using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [SerializeField]
    private GameObject[] topAndBottomBricks, leftBricks, rightBricks;

    public GameObject panelBG, levelFinishedPanel, playerDiedPanel, pausePanel;

    private GameObject topBrick, bottomBrick, leftBrick, rightBrick;

    private Vector3 coordinates;

    [SerializeField]
    private GameObject[] players;

    public float levelTime;

    public Text liveText, scoreText, levelTimerText, showScoreAtEndOfLevelText, countDownAndBeginLevelText, watchVideoText;

    private float countDownBeforeLevelBegins = 3.0f;

    public static int smallBallCount = 0;

    public int playerLives, playerScore, coins;

    private bool isGamePaused, hasLevelBegan, countDownLevel;

    public bool levelInProgress;

    [SerializeField]
    private GameObject[] endOfLevelRewards;

    [SerializeField]
    private Button pauseButton;

    private void Awake()
    {
        CreateInstance();
        InitializeBrickAndPlayer();

    }

    private void Start()
    {
        InitializeGameplayController();
    }

    private void Update()
    {
        UpdateGameplayController();
    }
    void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

 

    void InitializeBrickAndPlayer()
    {
        coordinates = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        int index = Random.Range(0, topAndBottomBricks.Length);
        
        topBrick = Instantiate(topAndBottomBricks[index]);
        bottomBrick = Instantiate(topAndBottomBricks[index]);

        leftBrick = Instantiate(leftBricks[index], new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,0,-90))) as GameObject;
        rightBrick = Instantiate(rightBricks[index], new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;
        topBrick.tag = "TopBrick";

        topBrick.transform.position = new Vector3(-coordinates.x + 3.5f, coordinates.y + 0.17f, 0);
        bottomBrick.transform.position = new Vector3(-coordinates.x + 3.5f, -coordinates.y - 0.2f, 0);
        leftBrick.transform.position = new Vector3(-coordinates.x - 0.17f, coordinates.y + 4, 0);
        rightBrick.transform.position = new Vector3(coordinates.x + 0.17f, coordinates.y - 7, 0);

        Instantiate(players[GameController.instance.selectedPlayer]);
    }

    void InitializeGameplayController()
    {
        if(GameController.instance.isGameStartedFromLevelMenu)
        {
            playerScore = 0;
            playerLives = 2;
            GameController.instance.currentScore = playerScore;
            GameController.instance.currentLives = playerLives;
            GameController.instance.isGameStartedFromLevelMenu = false;
            
        }

        else
        {
            playerScore = GameController.instance.currentScore;
            playerLives = GameController.instance.currentLives;
        }

        levelTimerText.text = levelTime.ToString("F0");
        scoreText.text = "Score x" + playerScore;
        liveText.text = "x" + playerLives;

        Time.timeScale = 0;
        countDownAndBeginLevelText.text = countDownBeforeLevelBegins.ToString("F0");
    }


    public void SetHasLevelBegan(bool hasLevelBegan)
    {
        this.hasLevelBegan = hasLevelBegan;
    }

    void UpdateGameplayController()
    {
        scoreText.text = "Score x" + playerScore;

        if(hasLevelBegan)
        {
            CountDownAndBeginLevel();
        }

        if(countDownLevel)
        {
            LevelCountDownTimer();
        }
    }



    void CountDownAndBeginLevel()
    {
        countDownBeforeLevelBegins -= (0.19f * 0.15f);
        countDownAndBeginLevelText.text = countDownBeforeLevelBegins.ToString("F0");

        if(countDownBeforeLevelBegins <=0)
        {
            Time.timeScale = 1;
            hasLevelBegan = false;
            countDownLevel = true;
            countDownAndBeginLevelText.gameObject.SetActive(false);
        }
    }

    void LevelCountDownTimer()
    {
        if(Time.timeScale == 1)
        {
            levelTime -= Time.deltaTime;
            levelTimerText.text = levelTime.ToString("F0");

            if(levelTime <= 0)
            {
                playerLives--;
                GameController.instance.currentLevel = playerLives;
                GameController.instance.currentScore = playerScore;

                if(playerLives < 0)
                {
                    StartCoroutine(PromptTheUserToWatchAVideo());
                }
                else
                {
                    StartCoroutine(PlayerDiedRestartLevel());
                }
            }
        }
    }


    


    public void PlayerDied()
    {
        countDownLevel = false;
        pauseButton.interactable = false;
        levelInProgress = false;

        smallBallCount = 0;
        playerLives--;
        GameController.instance.currentLives = playerLives;
        GameController.instance.currentScore = playerScore;

        if (playerLives < 0)
        {
            StartCoroutine(PromptTheUserToWatchAVideo());
        }
        else
        {
            StartCoroutine(PlayerDiedRestartLevel());
        }
    }

    public void CountSmallBalls()
    {
        smallBallCount--;
        if(smallBallCount == 0 )
        {
            StartCoroutine(LevelCompleted());
        }
    }

    public void GoToMapButton()
    {
        GameController.instance.currentScore = playerScore;

        if(GameController.instance.highScore < GameController.instance.currentScore)
        {
            GameController.instance.highScore = GameController.instance.currentScore;
            GameController.instance.Save();
        }

        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;

        }

        SceneManager.LoadScene("LevelMenu");

        if(LoadingScreen.instance != null)
        {
            LoadingScreen.instance.PlayLoadingScreen();
        }

    }

    public void RestartCurrentlevelButton()
    {
        smallBallCount = 0;
        coins = 0;

        GameController.instance.currentLives = playerLives;
        GameController.instance.currentScore = playerScore;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if(LoadingScreen.instance != null)
        {
            LoadingScreen.instance.PlayLoadingScreen();
        }


    }

    public void Nextlevel()
    {
        GameController.instance.currentScore = playerScore;
        GameController.instance.currentLives = playerLives;

        if(GameController.instance.highScore < GameController.instance.currentScore)
        {
            GameController.instance.highScore = GameController.instance.currentScore;
            GameController.instance.Save();
        }

        int nextLevel = GameController.instance.currentLevel;
        nextLevel++;

        if(!(nextLevel >= GameController.instance.levels.Length))
        {
            GameController.instance.currentLevel = nextLevel;

            SceneManager.LoadScene("Level" + nextLevel);

            if (LoadingScreen.instance != null)
            {
                LoadingScreen.instance.PlayLoadingScreen();
            }
        }
    }


    public void PauseGame()
    {
        if(!hasLevelBegan)
        {
            if(levelInProgress)
            {
                if(!isGamePaused)
                {
                    countDownLevel = false;
                    levelInProgress = false;
                    isGamePaused = true;

                    panelBG.SetActive(true);
                    pausePanel.SetActive(true);

                    Time.timeScale = 0;
                }
            }
        }
    }

    public void ResumeGame()
    {
        countDownLevel = true;
        levelInProgress = true;
        isGamePaused = false;

        panelBG.SetActive(false);
        pausePanel.SetActive(false);

        Time.timeScale = 1;
    }



    public void DontWatchVideoInsteadQuit()
    {
        GameController.instance.currentScore = playerScore;

        if(GameController.instance.highScore < GameController.instance.currentScore)
        {
            GameController.instance.highScore = GameController.instance.currentScore;

            GameController.instance.Save();
        }

        Time.timeScale = 1f;

        SceneManager.LoadScene("LevelMenu");

        if(LoadingScreen.instance != null)
        {
            LoadingScreen.instance.PlayLoadingScreen();
        }
    }

    IEnumerator PlayerDiedRestartLevel()
    {
        levelInProgress = false;
        coins = 0;
        smallBallCount = 0;

        Time.timeScale = 0;
        if (LoadingScreen.instance != null)
        {
            LoadingScreen.instance.FadeOut();
        }

        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1.25f));

        //reload current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (LoadingScreen.instance != null)
        {
            LoadingScreen.instance.PlayFadeInAnimation();
        }


    }

    IEnumerator PromptTheUserToWatchAVideo()
    {
        levelInProgress = false;
        countDownLevel = false;
        pauseButton.interactable = false;

        Time.timeScale = 0;
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(0.7f));

        playerDiedPanel.SetActive(true);
    }

    IEnumerator GivePlayerLivesRewardAfterWatchingVideo()
    {
        watchVideoText.text = "Thank Yout For Watching The Video. You Earn 2 Extra Lives";
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(2f));

        coins = 0;
        playerLives = 2;
        smallBallCount = 0;

        GameController.instance.currentLives = playerLives;

        GameController.instance.currentScore = playerScore;
        Time.timeScale = 0;

        if (LoadingScreen.instance != null)
        {
            LoadingScreen.instance.FadeOut();
        }

        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1.25f));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        if (LoadingScreen.instance != null)
        {
            LoadingScreen.instance.PlayFadeInAnimation();
        }
    }


    IEnumerator LevelCompleted()
    {
        countDownLevel = false;
        pauseButton.interactable = false;

        int unlockLevel = GameController.instance.currentLevel;
        unlockLevel++;

        if(!(unlockLevel >= GameController.instance.levels.Length))
        {
            GameController.instance.levels[unlockLevel] = true;
            
        }


        Instantiate(endOfLevelRewards[GameController.instance.currentLevel], new Vector3(0, Camera.main.orthographicSize, 0), Quaternion.identity);


        if(GameController.instance.doubelCoins)
        {
            coins *= 2;
        }

        GameController.instance.coins = coins;
        GameController.instance.Save();

        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(4f));
        levelInProgress = false;
        PlayerScript.instance.StopMoving();
        Time.timeScale = 0;
        levelFinishedPanel.SetActive(true);
        showScoreAtEndOfLevelText.text = "" + playerScore;
    }



}
