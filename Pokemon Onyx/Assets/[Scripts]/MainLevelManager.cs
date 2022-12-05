using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLevelManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBgm("MainBgm");
    }


    // for testing
    //void Update()
    //{
    //    if (Input.GetKeyDown("escape"))
    //    {
    //        SoundManager.Instance.PlayBgm("MainBgm");
    //        SceneManager.UnloadSceneAsync("EncounterScene");
    //        FindObjectOfType<PlayerController>().encounterActive = false;
    //    }

    //}

}
