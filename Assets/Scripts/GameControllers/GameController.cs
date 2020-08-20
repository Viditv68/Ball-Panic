using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private GameData data;

    public int currentLevel;
    public int currentScore;

    public bool isGameStartedFirstTime;
    public bool isMusicOn;

    public int selectedPlayer;
    public int selectedWeapon;
    public int coins;
    public int highScore;

    public bool[] players;
    public bool[] levels;
    public bool[] weapons;
    public bool[] achievements;
    public bool[] collectedItems;


    private void Awake()
    {
        MakeSingleton();
        InitializeGameVariables();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void InitializeGameVariables()
    {
        Load();

        if(data != null)
        {
            isGameStartedFirstTime = data.GetIsGameStartedFirstTime();

        }
        else
        {
            isGameStartedFirstTime = true;
        }

        if(isGameStartedFirstTime)
        {
            highScore = 0;
            coins = 0;

            isGameStartedFirstTime = false;
            isMusicOn = false;

            players = new bool[6];
            levels = new bool[40];
            weapons = new bool[4];
            achievements = new bool[8];
            collectedItems = new bool[40];

            players[0] = true;
            for(int i = 1; i < players.Length; i++)
            {
                players[i] = false;
            }

            levels[0] = true;
            for (int i = 1; i < levels.Length; i++)
            {
                levels[i] = false;
            }

            weapons[0] = true;
            for (int i = 1; i < weapons.Length; i++)
            {
                weapons[i] = false;
            }

            for (int i = 0; i < achievements.Length; i++)
            {
                achievements[i] = false;
            }

            for (int i = 0; i < collectedItems.Length; i++)
            {
                collectedItems[i] = false;
            }

            data = new GameData();

            data.SetHighScore(highScore);
            data.SetCoins(coins);
            data.setIsGameStartedFirstTime(isGameStartedFirstTime);
            data.SetPlayers(players);
            data.SetLevels(levels);
            data.SetWeapons(weapons);
            data.SetSelectedPlayer(selectedPlayer);
            data.SetSelectedWeapon(selectedWeapon);
            data.SetIsMusicOn(isMusicOn);
            data.SetAchievements(achievements);
            data.SetCollectedItems(collectedItems);

            Save();
            Load(); 
        }

        else
        {
            highScore = data.GetHighScore();
            coins = data.GetCoins();
            selectedPlayer = data.GetSelectedPlayer();
            selectedWeapon = data.GetSelectedWeapon();
            isGameStartedFirstTime = data.GetIsGameStartedFirstTime();
            isMusicOn = data.GetIsMusicOn();
            players = data.GetPlayers();
            levels = data.GetLevels();
            weapons = data.GetWeapons();
            achievements = data.GetAchievements();
            collectedItems = data.GetCollectedItems();


        }
    }


    public void Save()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/GameData.dat"); 

            if(data != null)
            {
                data.SetHighScore(highScore);
                data.SetCoins(coins);
                data.setIsGameStartedFirstTime(isGameStartedFirstTime);
                data.SetPlayers(players);
                data.SetLevels(levels);
                data.SetWeapons(weapons);
                data.SetSelectedPlayer(selectedPlayer);
                data.SetSelectedWeapon(selectedWeapon);
                data.SetIsMusicOn(isMusicOn);
                data.SetAchievements(achievements);
                data.SetCollectedItems(collectedItems);

                bf.Serialize(file, data);
            }
        }
        catch(Exception e)
        {

        }
        finally
        {
            if(file != null)
                file.Close();
        }
    }



    public void Load()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);

            data = (GameData)bf.Deserialize(file);
        }
        catch(Exception e)
        {

        }
        finally
        {
            if(file != null)
            {
                file.Close();
            }
        }
    }
   
}

[Serializable]
class GameData
{
    private bool isGameStartedFirstTime;
    private bool isMusicOn;
    
    private int selectedPlayer;
    private int selectedWeapon;
    private int coins;
    private int highScore;

    private bool[] players;
    private bool[] levels;
    private bool[] weapons;
    private bool[] achievements;
    private bool[] collectedItems;

    public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        this.isGameStartedFirstTime = isGameStartedFirstTime;
    }

    public bool GetIsGameStartedFirstTime()
    {
        return this.isGameStartedFirstTime;
    }

    public void SetIsMusicOn(bool isMusicOn)
    {
        this.isMusicOn = isMusicOn;
    }

    public bool GetIsMusicOn()
    {
        return this.isMusicOn;
    }

    public void SetSelectedPlayer(int selectedPlayer)
    {
        this.selectedPlayer = selectedPlayer;
    }

    public int GetSelectedPlayer()
    {
        return this.selectedPlayer;
    }

    public void SetSelectedWeapon(int selectedWeapon)
    {
        this.selectedWeapon = selectedWeapon;
    }

    public int GetSelectedWeapon()
    {
        return this.selectedWeapon;
    }


    public void SetCoins(int coins)
    {
        this.coins = coins;
    }
    
    public int GetCoins()
    {
        return this.coins;
    }

    public void SetHighScore(int highScore)
    {
        this.highScore = highScore;
    }

    public int GetHighScore()
    {
        return this.highScore;
    }

    public void SetPlayers(bool[] players)
    {
        this.players = players;
    }

    public bool[] GetPlayers()
    {
        return this.players;
    }

    public void SetLevels(bool[] levels)
    {
        this.levels = levels;
    }

    public bool[] GetLevels()
    {
        return this.levels;
    }

    public void SetWeapons(bool[] weapons)
    {
        this.weapons = weapons;
    }

    public bool[] GetWeapons()
    {
        return this.weapons;
    }

    public void SetAchievements(bool[] achievements)
    {
        this.achievements = achievements;
    }

    public bool[] GetAchievements()
    {
        return this.achievements;
    }

    public void SetCollectedItems(bool[] collectedItems)
    {
        this.collectedItems = collectedItems;
    }

    public bool[] GetCollectedItems()
    {
        return this.collectedItems; 
    }


}
