using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    //public Transform playerTransform;
    public int HP;
    public int MaxHP;
    public int Mana;
    public int MaxMana;

    public PlayerData(Player player)
    {
        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;

        HP = player.status.HP;
        MaxHP = player.status.MaxHP;
        Mana = player.status.Mana;
        MaxMana = player.status.MaxMana;
        //playerTransform = player.transform;
    }
    
}
