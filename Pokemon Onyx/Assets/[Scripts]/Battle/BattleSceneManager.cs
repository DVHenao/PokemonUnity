using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    public StatusUI playerStatusUI;
    public StatusUI EnemyStatusUI;

    public GameObject PlayerSkillPannel;
    public GameObject EnemySkillPannel;

    public TMPro.TextMeshProUGUI BattleDescrition;
   

    public GameObject SkillButtonPrefab;

    public BattleManager battleManager;


    public void UpdateStatusUI()
    {
        playerStatusUI.SetHpSlider(battleManager.Player.status.HP, battleManager.Player.status.MaxHP);
        playerStatusUI.SetMpSlider(battleManager.Player.status.Mana, battleManager.Player.status.MaxMana);

        EnemyStatusUI.SetHpSlider(battleManager.Enemy.status.HP, battleManager.Enemy.status.MaxHP);
        EnemyStatusUI.SetMpSlider(battleManager.Enemy.status.Mana, battleManager.Enemy.status.MaxMana);

    }


    public void SetTurn(Turn turn)
    {
        switch (turn)
        {
            case Turn.PLAYER:
                //GameDesc.text = "Player Turn";
                PlayerSkillPannel.SetActive(true);
                //EnemySkillPannel.SetActive(false);
                break;
            case Turn.ENEMY:
                //GameDesc.text = "Enemy Turn";
                PlayerSkillPannel.SetActive(false);
                //EnemySkillPannel.SetActive(true);
                break;
        }
    }

    public void SetClickPannelDisable(Turn turn)
    {
        switch (turn)
        {
            case Turn.PLAYER:
                PlayerSkillPannel.SetActive(false);
                break;
        }
    }

    public void UnLoadScene()
    {
        //SoundManager.Instance.PlayBgm("PlayScene", 2.0f);
        SceneManager.UnloadScene("EncounterScene");
    }
}
