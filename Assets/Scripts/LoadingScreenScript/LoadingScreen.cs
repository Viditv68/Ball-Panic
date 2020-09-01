using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;

    [SerializeField]
    private GameObject bgImage, logoImage, text, fadePanel;

    [SerializeField]
    private Animator fadeAnim;


    void Awake()
    {
        MakeSingleton();
        Hide();
    }

    private void MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Show()
    {
        bgImage.SetActive(true);
        logoImage.SetActive(true);
        text.SetActive(true);
        
    }

    private void Hide()
    {
        bgImage.SetActive(false);
        logoImage.SetActive(false);
        text.SetActive(false);
    }

    public void PlayLoadingScreen()
    {
        StartCoroutine(ShowLoadingScreen());
    }


    public void PlayFadeInAnimation()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        fadeAnim.Play("FadeIn");
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(0.4f));

        if(GameplayController.instance != null)
        {
            GameplayController.instance.SetHasLevelBegan(true);
        }

        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(0.9f));
        fadePanel.SetActive(false);
    }

    public void FadeOut()
    {
        fadePanel.SetActive(true);
        fadeAnim.Play("FadeOut");
    }



    IEnumerator ShowLoadingScreen()
    {
        Debug.Log("hello");
        Show();
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1f));
        Hide();

        if(GameplayController.instance!=null)
        {
            GameplayController.instance.SetHasLevelBegan(true);
        }


    }



}
