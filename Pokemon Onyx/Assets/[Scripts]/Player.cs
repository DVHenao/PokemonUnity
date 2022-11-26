using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Status status;

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
}
