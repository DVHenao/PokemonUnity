using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public GameObject credit;
    public GameObject LoadingPannel;

    public Slider loadingSlider;

    public Button continueButton;


    public void Start()
    {
        SoundManager.Instance.PlayBgm("TitleBgm");

        if(!SaveSystem.DoesSaveExist())
        {
            continueButton.interactable = false;
        }

    }


    public void playGame()
    {
        Debug.Log("pressed");
        SoundManager.Instance.PlayFX("Click");
        StartCoroutine(LoadScene());
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("continue", 1);
        SoundManager.Instance.PlayFX("Click");
        StartCoroutine(LoadScene());
    }

    public void Credit()
    {
        SoundManager.Instance.PlayFX("Click");
        credit.SetActive(true);
    }

    public void quitGame()
    {
        Debug.Log("pressed");
        SoundManager.Instance.PlayFX("Click");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ClickSound()
    {
        SoundManager.Instance.PlayFX("Click");
    }

    IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainGame");

        LoadingPannel.SetActive(true);

        while(!operation.isDone)
        {
            loadingSlider.value = operation.progress / 0.9f;
            yield return null;
        }
    }
}
