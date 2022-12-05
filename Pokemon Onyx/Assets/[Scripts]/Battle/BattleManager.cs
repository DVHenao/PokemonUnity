using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Turn
{
    PLAYER,
    ENEMY
}
public class BattleManager : MonoBehaviour
{

    public List<GameObject> enemyPrefabs;

    public BattleSceneManager battleSceneManager;
    public BattleAnimationController battleAnimationController;

    public Transform EnemyParent;

    public Character Player;
    public Enemy Enemy;

    


    public Turn CurrentTurn;

    // Start is called before the first frame update
    private void Awake()
    {
        // set Player
        Player.status = FindObjectOfType<DataTransfer>().playerStatus;
        
        int random = Random.Range(0, enemyPrefabs.Count);
        var obj = Instantiate(enemyPrefabs[random], EnemyParent);
        Enemy = obj.GetComponent<Enemy>();
    }

    
    void Start()
    {
        // for setting skill button
        // by using player skill, instantiate buttons, and setting
        foreach (var skill in Player.status.AttackSkillList)
        {
            GameObject btn = Instantiate(battleSceneManager.SkillButtonPrefab, battleSceneManager.PlayerSkillPannel.transform);
            SkillButtonUI skillBtn = btn.GetComponent<SkillButtonUI>();
            skillBtn.SetButtonType(SkillButtonUI.ButtonType.ATTACK);
            skillBtn.SetSkillNameText(skill.skillName);
            skillBtn.SetCostText(skill.manaCost.ToString());
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (Player.status.Mana >= skill.manaCost)
                {
                    battleSceneManager.BattleDescrition.color = Color.green;
                    battleSceneManager.BattleDescrition.text = "Player " + skill.Effect;
                    Player.status.SelectedEffectPrefab = skill.EffectPrefab;
                    Player.status.SelectedSoundName = skill.soundName;
                    Player.status.UseMana(skill.manaCost);
                    Enemy.status.Damaged(skill.damageValue);

                    SoundManager.Instance.PlayFX("ClickSkill", 0.5f);
                    Player.GetComponent<Animator>().Play("Shaking", 0, 0.0f);
                    StartCoroutine(StartBattleSequenc(skill.type));
                }
            });

        }
        foreach (var skill in Player.status.CureSkillList)
        {
            GameObject btn = Instantiate(battleSceneManager.SkillButtonPrefab, battleSceneManager.PlayerSkillPannel.transform);
            SkillButtonUI skillBtn = btn.GetComponent<SkillButtonUI>();
            skillBtn.SetButtonType(SkillButtonUI.ButtonType.HEAL);
            skillBtn.SetSkillNameText(skill.skillName);
            skillBtn.SetCostText(skill.manaCost.ToString());
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (Player.status.Mana >= skill.manaCost)
                {
                    battleSceneManager.BattleDescrition.color = Color.green;
                    battleSceneManager.BattleDescrition.text = skill.Effect;
                    Player.status.SelectedEffectPrefab = skill.EffectPrefab;
                    Player.status.SelectedSoundName = skill.soundName;
                    Player.status.UseMana(skill.manaCost);
                    Player.status.Heal(skill.healValue);

                    SoundManager.Instance.PlayFX("ClickSkill", 0.5f);
                    StartCoroutine(StartBattleSequenc(skill.type));
                }
            });

        }

        // setting enemy status
        battleSceneManager.EnemyStatusUI.SetName(Enemy.Name);
        battleSceneManager.EnemyStatusUI.SetHPText(Enemy.status.HP, Enemy.status.MaxHP);
        battleSceneManager.EnemyStatusUI.SetMPText(Enemy.status.Mana, Enemy.status.MaxMana);

        // first turn
        CurrentTurn = Turn.PLAYER;

        // Init UI
        battleSceneManager.UpdateStatusUI();
    }

    //private void Update()
    //{
    //    // for testing
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        Player.status.HP = 5;
    //    }

    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        Enemy.status.HP = 5;
    //    }

    //}

    // When click skill button, It'll work
    public IEnumerator StartBattleSequenc(SkillType skillType)
    {
        battleSceneManager.SetClickPannelDisable(CurrentTurn);
        yield return new WaitForSeconds(1.0f);
        battleAnimationController.PlayEffectAnimation(CurrentTurn, skillType);
        battleSceneManager.UpdateStatusUI();
        yield return new WaitForSeconds(1.0f);
        battleSceneManager.BattleDescrition.text = "";
        CheckResult();
    }

    public void ChangedTurn()
    {
        CurrentTurn = CurrentTurn == Turn.PLAYER ? Turn.ENEMY : Turn.PLAYER;
        battleSceneManager.SetTurn(CurrentTurn);

        // if enemy turn, excuting enemy AI
        if (CurrentTurn == Turn.ENEMY)
        {
            Enemy.ExcuteAI();
        }
    }

    // play SFX/VFX
    

    public void CheckResult()
    {
        if (Enemy.status.HP <= 0 || Player.status.HP <= 0)
        {
            // quit secuence
            StopAllCoroutines();
            SoundManager.Instance.PlayBgm("MainBgm");
            battleSceneManager.SetClickPannelDisable(Turn.PLAYER);

            if (Enemy.status.HP <= 0)
            {
                battleSceneManager.BattleDescrition.text = "Player Win ";
                SoundManager.Instance.PlayFX("Win", 0.5f);
                Player.status.ChaargeMana(Player.status.Mana / 2);
            }
            else
            {
                battleSceneManager.BattleDescrition.text = "Player Lose";
                SoundManager.Instance.PlayFX("Lose",0.5f);
            }
            StartCoroutine(QuitBattleSequence());
        }
        else
        {
            ChangedTurn();
        }
    }

    IEnumerator QuitBattleSequence()
    {
        yield return new WaitForSeconds(2.0f);
        
        if (Player.status.HP <= 0)
        {
            if (SaveSystem.DoesSaveExist())
            {
                FindObjectOfType<Player>().LoadPlayer();
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
                yield break;
            }
        }
        FindObjectOfType<PlayerController>().encounterActive = false;
        battleSceneManager.UnLoadScene();
    }
}
