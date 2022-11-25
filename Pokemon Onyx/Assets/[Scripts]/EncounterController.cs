using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterController : MonoBehaviour
{
    [SerializeField]
    public GameObject MainCamera;
    [SerializeField]
    public GameObject Canvas;
    [SerializeField]
    public GameObject playerController;

    public GameObject MainPokemon;

    // Start is called before the first frame update
    void Start()
    {
        int whatPokemon = Random.Range(1, 6);

        switch (whatPokemon) // load random pokemon
        { 
            case 1:
                //MainPokemon.AddComponent()
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            SoundManager.Instance.PlayBgm("MainBgm");
            SceneManager.UnloadSceneAsync("EncounterScene");
 
            PlayerController.encounterActive = false;

        }

    }
}
