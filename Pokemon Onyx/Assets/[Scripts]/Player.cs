using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Status status;

    public GameObject optionPannel;
    public Toggle optionToggle;

    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.GetInt("continue") == 1)
        {
            LoadPlayer();
            PlayerPrefs.SetInt("continue", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePlayer()
    {
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
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnClickOption(Toggle toggle)
    {
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
