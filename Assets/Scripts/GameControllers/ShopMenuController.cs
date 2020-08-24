using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopMenuController : MonoBehaviour
{

    public static ShopMenuController instance;

    public Text coinText, scoretext, buyArrowText, watchVideoText;

    public Button weaponsTabButton, specialtabButton, earnCoinTabButton, yesButton;

    public GameObject weaponItemspanel, specialItemsPanel, earnCoinsPanel, coinShopPanel, buyArrowsPanel;

    // Start is called before the first frame update
    void Awake()
    {
        MoveInstance();
    }

    private void Start()
    {
        InitializeShopMenuController();
    }

    private void MoveInstance()
    {
        if(instance == null)
        {
            instance = this;
        }

    }


    public void BuyDoubleArrows()
    {
        CanBuyArrows(1);
    }

    public void BuyStickyArrows()
    {
        CanBuyArrows(2);
    }

    public void BuyDoubleStickyArrows()
    {
        CanBuyArrows(3);
    }


    private void CanBuyArrows(int index)
    {
        if (!GameController.instance.weapons[index])
        {
            if (GameController.instance.coins >= 7000)
            {
                buyArrowsPanel.SetActive(true);
                buyArrowText.text = "Do you want to purchase double arrows";
                yesButton.onClick.RemoveAllListeners();
                yesButton.onClick.AddListener(() => BuyArrow(index));
            }
            else
            {
                buyArrowsPanel.SetActive(true);
                buyArrowText.text = "You don't have enough coins. Do you want to buy coins ?";
                yesButton.onClick.RemoveAllListeners();
                yesButton.onClick.AddListener(() => OpenCoinShop());
            }
        }
    }


    public void BuyArrow(int index)
    {
        GameController.instance.weapons[index] = true;
        GameController.instance.coins -= 7000;
        GameController.instance.Save();

        buyArrowsPanel.SetActive(false);
        coinText.text = "" + GameController.instance.coins;
    }

    void InitializeShopMenuController()
    {
        coinText.text = "" + GameController.instance.coins;
        scoretext.text = "" + GameController.instance.highScore;
    }

    public void OpenCoinShop()
    {
        if(buyArrowsPanel.activeInHierarchy)
        {
            buyArrowsPanel.SetActive(false);
        }
        coinShopPanel.SetActive(true);
    }

    public void CloseCoinSHop()
    {
        coinShopPanel.SetActive(false);
    }

    public void openWeaponItemsPanel()
    {
        weaponItemspanel.SetActive(true);
        specialItemsPanel.SetActive(false);
        earnCoinsPanel.SetActive(false);
    }

    public void OpenSpecialItemspanel()
    {
        specialItemsPanel.SetActive(true);
        weaponItemspanel.SetActive(false); 
        earnCoinsPanel.SetActive(false);
    }

    public void OpenEarnCoinsItemPanel()
    {
        earnCoinsPanel.SetActive(true);
        specialItemsPanel.SetActive(false);
        weaponItemspanel.SetActive(false);
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("PlayerMenu");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void DontBuyArrows()
    {
        buyArrowsPanel.SetActive(false);
    }

}
