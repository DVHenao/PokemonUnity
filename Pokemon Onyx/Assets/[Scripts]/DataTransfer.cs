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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerStatusForBattle()
    {
        playerStatus = GameObject.Find("Player").GetComponent<Player>().status;
    }
}
