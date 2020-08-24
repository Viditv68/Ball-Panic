using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public Text scoreText, coinText;

    public bool[] levels;

    public Button[] levelButtons;

    public Text[] levelText;

    public Image[] lockIcons;

    public GameObject coinShopPanel;
    // Start is called before the first frame update
    void Start()
    {
        InitializeLevelMenu();
    }

    private void InitializeLevelMenu()
    {
        scoreText.text = "" + GameController.instance.highScore;
        coinText.text = "" + GameController.instance.coins;

        levels = GameController.instance.levels;

        for(int i = 1; i < levels.Length; i++)
        {
            if(levels[i])
            {
                lockIcons[i - 1].gameObject.SetActive(false);
            }
            else
            {
                levelButtons[i - 1].interactable = false;
                levelText[i - 1].gameObject.SetActive(false);
            }
        }
    }

    public void LoadLevel()
    {
        if(GameController.instance.isMusicOn)
        {
            MusicController.instance.GameIsLoadedTurnOffMusic();
        }

        string level = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        switch(level)
        {
            case "Level 0":
                GameController.instance.currentLevel = 0;
                break;
            case "Level 1":
                GameController.instance.currentLevel = 1;
                break;
            case "Level 2":
                GameController.instance.currentLevel = 2;
                break;
            case "Level 3":
                GameController.instance.currentLevel = 3;
                break;
            case "Level 4":
                GameController.instance.currentLevel = 4;
                break;
            case "Level 5":
                GameController.instance.currentLevel = 5;
                break;
            case "Level 6":
                GameController.instance.currentLevel = 6;
                break;
            case "Level 7":
                GameController.instance.currentLevel = 7;
                break;
            case "Level 8":
                GameController.instance.currentLevel = 8;
                break;
            case "Level 9":
                GameController.instance.currentLevel = 9;
                break;
            case "Level 10":
                GameController.instance.currentLevel = 10;
                break;
            case "Level 11":
                GameController.instance.currentLevel = 11;
                break;
            case "Level 12":
                GameController.instance.currentLevel = 12;
                break;
            case "Level 13":
                GameController.instance.currentLevel = 13;
                break;
            case "Level 14":
                GameController.instance.currentLevel = 14;
                break;

        }
        LoadingScreen.instance.PlayLoadingScreen();
        SceneManager.LoadScene(level);
    }
    
    public void OpenCoinShop()
    {
        coinShopPanel.SetActive(true);
    }

    public void CloseCoinShop()
    {
        coinShopPanel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoBackButton()
    {
        SceneManager.LoadScene("PlayerMenu");
    }
}
