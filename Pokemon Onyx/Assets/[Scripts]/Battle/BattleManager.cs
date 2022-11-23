using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public enum Turn
{
    PLAYER,
    ENEMY
}
public class BattleManager : MonoBehaviour
{
    

    public BattleSceneManager battleSceneManager;

    public Character Player;
    public Enemy Enemy;

    public Turn CurrentTurn;

    // Start is called before the first frame update
    private void Awake()
    {
        // set Player
        Player.status = FindObjectOfType<DataTransfer>().playerStatus;
    }

    
    void Start()
    {
        
        foreach (var skill in Player.status.AttackSkillList)
        {
            GameObject btn = Instantiate(battleSceneManager.SkillButtonPrefab, battleSceneManager.PlayerSkillPannel.transform);
            btn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = skill.skillName;
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                battleSceneManager.PlayerDesc.text = skill.Effect;
                Player.status.Mana -= skill.manaCost;
                Enemy.status.HP -= skill.damageValue;
                StartCoroutine(SetTurnSetting());
            });

        }
        foreach (var skill in Player.status.CureSkillList)
        {
            GameObject btn = Instantiate(battleSceneManager.SkillButtonPrefab, battleSceneManager.PlayerSkillPannel.transform);
            btn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = skill.skillName;
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                battleSceneManager.PlayerDesc.text = skill.Effect;
                Player.status.Mana -= skill.manaCost;
                Player.status.HP += skill.healValue;
                StartCoroutine(SetTurnSetting());
            });

        }

        //foreach (var skill in Enemy.status.AttackSkillList)
        //{
        //    GameObject btn = Instantiate(battleSceneManager.SkillButtonPrefab, battleSceneManager.EnemySkillPannel.transform);
        //    btn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = skill.skillName;
        //    btn.GetComponent<Button>().onClick.AddListener(() =>
        //    {
        //        battleSceneManager.EnemyDesc.text = skill.Effect;
        //        Player.status.HP -= skill.damageValue;
        //        StartCoroutine(SetTurnSetting());
        //    });
        //}

        CurrentTurn = Turn.PLAYER;
        battleSceneManager.SetTurn(CurrentTurn);
    }

    private void Update()
    {
        if (Enemy.status.HP <= 0)
        {
            battleSceneManager.UnLoadScene();
        }

    }
    public IEnumerator SetTurnSetting()
    {
        battleSceneManager.SetClickPannelDisable(CurrentTurn);
        yield return new WaitForSeconds(2.0f);
        battleSceneManager.PlayerDesc.text = "";
        battleSceneManager.EnemyDesc.text = "";
        ChangedTurn();
        
    }

    public void ChangedTurn()
    {
        CurrentTurn = CurrentTurn == Turn.PLAYER ? Turn.ENEMY : Turn.PLAYER;
        battleSceneManager.SetTurn(CurrentTurn);
        if (CurrentTurn == Turn.ENEMY)
        {
            Enemy.ExcuteAI();
            StartCoroutine(SetTurnSetting());
        }
    }
}
