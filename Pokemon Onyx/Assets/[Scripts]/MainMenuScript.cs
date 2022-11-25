using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGame()
    {
        Debug.Log("pressed");
        SoundManager.Instance.PlayUI("Click");
        SceneManager.LoadScene("MainGame");
    }

    public void quitGame()
    {
        Debug.Log("pressed");
        SoundManager.Instance.PlayUI("Click");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
