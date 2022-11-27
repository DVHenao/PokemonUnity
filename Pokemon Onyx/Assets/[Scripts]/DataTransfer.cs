using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTransfer : MonoBehaviour
{

    public Status playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // when player encounter enemy, it'll be called
    public void SetPlayerStatusForBattle()
    {
        playerStatus = GameObject.Find("PlayerCharacter").GetComponent<Player>().status;
    }
}
