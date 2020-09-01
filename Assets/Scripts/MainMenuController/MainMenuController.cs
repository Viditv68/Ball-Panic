using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    
    [SerializeField]
    private Animator settingsButtonAnim;

    private bool hidden;
    private bool canTouchSettingsButton;

    [SerializeField]
    private Button musicButton;

    [SerializeField]
    private Sprite[] musicButtonSprite;

    [SerializeField]
    private Button fbButton;

    [SerializeField]
    private Sprite[] fbSprites;

    [SerializeField]
    private GameObject infoPanel;

    [SerializeField]
    private Image infoImage;

    [SerializeField]
    private Sprite[] infoSprites;

    private int infoIndex;

    void Start()
    {
        canTouchSettingsButton = true;
        hidden = true;

        if(GameController.instance.isMusicOn)
        {
            MusicController.instance.PlayBgMusic();
            musicButton.image.sprite = musicButtonSprite[0];
        }
        else
        {
            MusicController.instance.StopBgMusic();
            musicButton.image.sprite = musicButtonSprite[1];
        }

        infoIndex = 0;
        infoImage.sprite = infoSprites[infoIndex];
    }

   
    void Update()
    {
        
    }


    public void SettingsButton()
    {
        StartCoroutine(DissableSettingsButtonWhilePlayingAnimation());
    }



    public void MusicButton()
    {
        if(GameController.instance.isMusicOn)
        {
            musicButton.image.sprite = musicButtonSprite[1];
            MusicController.instance.StopBgMusic();
            GameController.instance.isMusicOn = false;
            GameController.instance.Save();

        }
        else
        {
            musicButton.image.sprite = musicButtonSprite[0];
            MusicController.instance.PlayBgMusic();
            GameController.instance.isMusicOn = true;
            GameController.instance.Save();
        }
    }


    public void OpenInfoPanel()
    {
        infoPanel.SetActive(true);
    }


    public void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
    }

    public void NextInfo()
    {
        infoIndex++;

        if(infoIndex == infoSprites.Length)
        {
            infoIndex = 0;
        }

        infoImage.sprite = infoSprites[infoIndex];
    }


    public void PlayButton()
    {
        MusicController.instance.PlayClickClip();
        SceneManager.LoadScene("PlayerMenu");
    }

    IEnumerator DissableSettingsButtonWhilePlayingAnimation()
    {
        if(canTouchSettingsButton)
        {
            if(hidden)
            {
                canTouchSettingsButton = false;
                settingsButtonAnim.Play("SlideIn");
                hidden = false;
                yield return new WaitForSeconds(1.2f);
                canTouchSettingsButton = true;
            }

            else
            {
                canTouchSettingsButton = false;
                settingsButtonAnim.Play("SlideOut");
                hidden = true;
                yield return new WaitForSeconds(1.2f);
                canTouchSettingsButton = true;
            }
        }
    }



}
