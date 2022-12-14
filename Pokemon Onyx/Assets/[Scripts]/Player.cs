using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Status status;

    public GameObject optionPannel;
    public Toggle optionToggle;

    void Start()
    {
        // for continue game
        if (PlayerPrefs.GetInt("continue") == 1)
        {
            LoadPlayer();
            PlayerPrefs.SetInt("continue", 0);
        }
    }


    public void SavePlayer()
    {
        SoundManager.Instance.PlayFX("Click");
        SaveSystem.SavePlayer(this);
        if (optionToggle.isOn)
        {
            optionToggle.isOn = false;
        }
    }

    public void LoadPlayer()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
        if (playerData != null)
        {
            Vector2 position = new Vector2(playerData.position[0], playerData.position[1]);
            transform.position = position;

            status.HP = playerData.HP;
            status.MaxHP = playerData.MaxHP;
            status.Mana = playerData.Mana;
            status.MaxMana = playerData.MaxMana;

        }

        if (optionToggle.isOn)
        {
            optionToggle.isOn = false;
        }
    }

    public void QuitGame()
    {
        SoundManager.Instance.PlayFX("Click");
        SceneManager.LoadSceneAsync("MainMenu");

    }

    public void OnClickOption(Toggle toggle)
    {
        SoundManager.Instance.PlayFX("Click");
        if(toggle.isOn)
        {
            optionPannel.SetActive(true);
        }
        else
        {
            optionPannel.SetActive(false);
        }

    }
}
