using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;

    [SerializeField]
    private GameObject bgImage, logoImage, text;


    void Awake()
    {
        MakeSingleton();
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
        ShowLoadingScreen();
    }

    IEnumerator ShowLoadingScreen()
    {
        Show();
        yield return new WaitForSeconds(2f);
        Hide();


    }



}
