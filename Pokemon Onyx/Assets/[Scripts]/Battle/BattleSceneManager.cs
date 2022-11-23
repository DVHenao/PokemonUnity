using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    public GameObject PlayerSkillPannel;
    public GameObject EnemySkillPannel;

    public TMPro.TextMeshProUGUI PlayerDesc;
    public TMPro.TextMeshProUGUI EnemyDesc;
    public TMPro.TextMeshProUGUI GameDesc;

    public Character Player;
    public Character Enemy;

    public GameObject SkillButtonPrefab;


    public void SetTurn(Turn turn)
    {
        //switch(turn)
        //{
        //    case Turn.PLAYER:
        //        GameDesc.text = "Player Turn";
        //        PlayerSkillPannel.SetActive(true);
        //        EnemySkillPannel.SetActive(false);
        //        break;
        //    case Turn.ENEMY:
        //        GameDesc.text = "Enemy Turn";
        //        PlayerSkillPannel.SetActive(false);
        //        EnemySkillPannel.SetActive(true);
        //        break;
        //}
    }

    public void SetClickPannelDisable(Turn turn)
    {
        //switch (turn)
        //{
        //    case Turn.PLAYER:
        //        PlayerSkillPannel.SetActive(false);
        //        break;
        //    case Turn.ENEMY:
        //        EnemySkillPannel.SetActive(false);
        //        break;
        //}
    }

    public void UnLoadScene()
    {
        //SoundManager.Instance.PlayBgm("PlayScene", 2.0f);
        SceneManager.UnloadScene("BattleScene");
    }
}
