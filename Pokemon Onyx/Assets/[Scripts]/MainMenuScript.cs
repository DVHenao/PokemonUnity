using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject credit;

    public void Start()
    {
        SoundManager.Instance.PlayBgm("TitleBgm");
    }


    public void playGame()
    {
        Debug.Log("pressed");
        SoundManager.Instance.PlayUI("Click");
        SceneManager.LoadScene("MainGame");
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("continue", 1);
        SoundManager.Instance.PlayUI("Click");
        SceneManager.LoadScene("MainGame");
    }

    public void Credit()
    {
        SoundManager.Instance.PlayUI("Click");
        credit.SetActive(true);
    }

    public void quitGame()
    {
        Debug.Log("pressed");
        SoundManager.Instance.PlayUI("Click");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ClickSound()
    {
        SoundManager.Instance.PlayUI("Click");
    }
}
