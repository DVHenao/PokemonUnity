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

    public List<GameObject> enemyPrefabs;

    public BattleSceneManager battleSceneManager;

    public Character Player;
    public Enemy Enemy;

    public Turn CurrentTurn;

    // Start is called before the first frame update
    private void Awake()
    {
        // set Player
        Player.status = FindObjectOfType<DataTransfer>().playerStatus;
        
        int random = Random.Range(0, enemyPrefabs.Count);
        var obj = Instantiate(enemyPrefabs[random]);
        Enemy = obj.GetComponent<Enemy>();
    }

    
    void Start()
    {
        
        foreach (var skill in Player.status.AttackSkillList)
        {
            GameObject btn = Instantiate(battleSceneManager.SkillButtonPrefab, battleSceneManager.PlayerSkillPannel.transform);
            SkillButtonUI skillBtn = btn.GetComponent<SkillButtonUI>();
            skillBtn.SetSkillNameText(skill.skillName);
            skillBtn.SetCostText(skill.manaCost.ToString());
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (Player.status.Mana >= skill.manaCost)
                {
                    battleSceneManager.BattleDescrition.text = "Player " + skill.Effect;
                    Player.status.UseMana(skill.manaCost);
                    Enemy.status.Damaged(skill.damageValue);
                    //battleSceneManager.UpdateStatusUI();
                    StartCoroutine(SetTurnSetting());
                }
            });

        }
        foreach (var skill in Player.status.CureSkillList)
        {
            GameObject btn = Instantiate(battleSceneManager.SkillButtonPrefab, battleSceneManager.PlayerSkillPannel.transform);
            SkillButtonUI skillBtn = btn.GetComponent<SkillButtonUI>();
            skillBtn.SetSkillNameText(skill.skillName);
            skillBtn.SetCostText(skill.manaCost.ToString());
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (Player.status.Mana >= skill.manaCost)
                {
                    battleSceneManager.BattleDescrition.text = "Player " + skill.Effect;
                    Player.status.UseMana(skill.manaCost);
                    Player.status.Heal(skill.healValue);
                    //battleSceneManager.UpdateStatusUI();
                    StartCoroutine(SetTurnSetting());
                }
            });

        }

        battleSceneManager.EnemyStatusUI.SetName(Enemy.Name);
        battleSceneManager.EnemyStatusUI.SetHPText(Enemy.status.HP, Enemy.status.MaxHP);
        battleSceneManager.EnemyStatusUI.SetMPText(Enemy.status.Mana, Enemy.status.MaxMana);



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
        //battleSceneManager.SetTurn(CurrentTurn);
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
        // animation
        yield return new WaitForSeconds(1.0f);
        battleSceneManager.UpdateStatusUI();
        yield return new WaitForSeconds(1.0f);
        battleSceneManager.BattleDescrition.text = "";
        //battleSceneManager.PlayerDesc.text = "";
        //battleSceneManager.EnemyDesc.text = "";
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
