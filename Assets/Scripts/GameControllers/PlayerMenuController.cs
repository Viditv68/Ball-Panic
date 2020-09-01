using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayerMenuController : MonoBehaviour
{

    public Text scoreText;
    public Text coinText;

    public bool[] players;
    public bool[] weapons;

    public Image[] priceTags;
    public Image[] weaponIcons;

    public Sprite[] weaponArrows;

    public int selectedWeapon;
    public int selectedPlayer;

    public GameObject buyPlayerPanel;

    public Button yesButton;

    public Text buyPlayerText;

    public GameObject coinShop;


    // Start is called before the first frame update
    void Start()
    {
        InitializePlayerMenuController();
    }

    private void InitializePlayerMenuController()
    {
        scoreText.text = "" + GameController.instance.highScore;
        coinText.text = "" + GameController.instance.coins;

        players = GameController.instance.players;
        weapons = GameController.instance.weapons;
        selectedPlayer = GameController.instance.selectedPlayer;
        selectedWeapon = GameController.instance.selectedWeapon;

        for(int i = 0; i < weaponIcons.Length; i++ )
        {
            weaponIcons[i].gameObject.SetActive(false);
        }

        for (int i = 1; i < players.Length; i++)
        {
            if(players[i] == true)
            {
                priceTags[i - 1].gameObject.SetActive(false);
            }
        }

        weaponIcons[selectedPlayer].gameObject.SetActive(true);
        weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

    }

    public void Player1Button()
    {
        CheckThePlayer(0);
    }

    public void PiratePlayerButton()
    {
        if(players[1] == true)
        {
            CheckThePlayer(1);
        }
        else
        {
            WantToPurchasePlayer(1);
        }
    }


    public void ZombiePlayerButton()
    {
        if (players[2] == true)
        {
            CheckThePlayer(2);
        }
        else
        {
            WantToPurchasePlayer(2);
        }
    }

    public void HomosapienlayerButton()
    {
        if (players[3] == true)
        {
            CheckThePlayer(3);
        }
        else
        {
            WantToPurchasePlayer(3);
        }
    }

    public void JokerPlayerButton()
    {
        if (players[4] == true)
        {
            CheckThePlayer(4);
        }
        else
        {
            WantToPurchasePlayer(4);
        }
    }

    public void SpartanPlayerButton()
    {
        if (players[5] == true)
        {
            CheckThePlayer(5);
        }
        else
        {
            WantToPurchasePlayer(5);
        }
    }




    private void CheckThePlayer(int index)
    {
        if (selectedPlayer != index)
        {
            selectedPlayer = index;
            selectedWeapon = 0;

            weaponIcons[selectedPlayer].gameObject.SetActive(true);
            weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

            for (int i = 0; i < weaponIcons.Length; i++)
            {
                if (i == selectedPlayer)
                {
                    continue;
                }

                weaponIcons[i].gameObject.SetActive(false);
            }

            GameController.instance.selectedPlayer = selectedPlayer;
            GameController.instance.selectedWeapon = selectedWeapon;
            GameController.instance.Save();
        }

        else
        {
            selectedWeapon++;
            if (selectedWeapon == weapons.Length)
            {
                selectedWeapon = 0;
            }

            bool foundWeapon = true;

            while (foundWeapon)
            {
                if (weapons[selectedWeapon] == true)
                {
                    weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
                    GameController.instance.selectedWeapon = selectedWeapon;
                    GameController.instance.Save();
                    foundWeapon = false;

                }
                else
                {
                    selectedWeapon++;
                    if (selectedWeapon == weapons.Length)
                    {
                        selectedWeapon = 0;
                    }

                }
            }
        }
    }

    private void WantToPurchasePlayer(int index)
    {
        if (GameController.instance.coins >= 7000)
        {
            buyPlayerPanel.SetActive(true);
            buyPlayerText.text = "Do You Want To Pruchase The Player ?";
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(() => BuyPlayer(index));

        }
        else
        {
            buyPlayerPanel.SetActive(true);
            buyPlayerText.text = "You Don't Have Enough Coins. Do You Want TO Buy the Coins ?";
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(() => OpenCoinShop());
        }
    }




    public void OpenCoinShop()
    {
        if(buyPlayerPanel.activeInHierarchy)
        {
            buyPlayerPanel.SetActive(false);

        }
        coinShop.SetActive(true);
    }

    public void CloseCoinShop()
    {
        coinShop.SetActive(false);
    }

    public void BuyPlayer(int index)
    {
        GameController.instance.players[index] = true;
        GameController.instance.coins -= 7000;
        GameController.instance.Save();
        InitializePlayerMenuController();

        buyPlayerPanel.SetActive(false);
    }

    public void DontBuyPlayerOrCoins()
    {
        buyPlayerPanel.SetActive(false);
    }

    public void GoToLevelMenu()
    {
        SceneManager.LoadScene("LevelMenu");
        
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
