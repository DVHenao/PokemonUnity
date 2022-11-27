using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public GameObject credit;

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
        SceneManager.LoadScene("MainGame");
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("continue", 1);
        SoundManager.Instance.PlayFX("Click");
        SceneManager.LoadScene("MainGame");
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
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ClickSound()
    {
        SoundManager.Instance.PlayFX("Click");
    }
}
