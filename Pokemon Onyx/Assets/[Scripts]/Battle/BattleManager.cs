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

    public Transform EnemyParent;

    public Character Player;
    public Enemy Enemy;

    public GameObject HitEffectPrefab;
    public Transform EffectTransform;


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
                    Player.status.UseMana(skill.manaCost);
                    Enemy.status.Damaged(skill.damageValue);
                    //battleSceneManager.UpdateStatusUI();
                    Player.GetComponent<Animator>().Play("Shaking", 0, 0.0f);
                    StartCoroutine(StartBattleSequenc());
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
                    Player.status.UseMana(skill.manaCost);
                    Player.status.Heal(skill.healValue);
                    //battleSceneManager.UpdateStatusUI();
                    StartCoroutine(StartHealSequenc());
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

        battleSceneManager.UpdateStatusUI();
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            Player.status.HP = 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Enemy.status.HP = 0;
        }

    }

    public IEnumerator StartBattleSequenc()
    {
        battleSceneManager.SetClickPannelDisable(CurrentTurn);
        // animation
        yield return new WaitForSeconds(1.0f);
        PlayHitEffect(CurrentTurn == Turn.PLAYER ? Enemy.transform : Player.transform);
        battleSceneManager.UpdateStatusUI();
        yield return new WaitForSeconds(1.0f);
        battleSceneManager.BattleDescrition.text = "";
        CheckResult();
    }

    public IEnumerator StartHealSequenc()
    {
        battleSceneManager.SetClickPannelDisable(CurrentTurn);
        // animation
        yield return new WaitForSeconds(1.0f);
        //PlayHitEffect(CurrentTurn == Turn.PLAYER ? Enemy.transform : Player.transform);
        battleSceneManager.UpdateStatusUI();
        yield return new WaitForSeconds(1.0f);
        battleSceneManager.BattleDescrition.text = "";
        CheckResult();
    }

    public void ChangedTurn()
    {
        CurrentTurn = CurrentTurn == Turn.PLAYER ? Turn.ENEMY : Turn.PLAYER;
        battleSceneManager.SetTurn(CurrentTurn);
        if (CurrentTurn == Turn.ENEMY)
        {
            Enemy.ExcuteAI();
            //StartCoroutine(SetTurnSetting());
        }
    }

    public void PlayHitEffect(Transform transform)
    {
        StartCoroutine(HitSequence(transform));
    }

    IEnumerator HitSequence(Transform transform)
    {
        Vector2 position1 = new Vector2(transform.position.x - 0.3f, transform.position.y + 0.4f);
        Vector2 position2 = new Vector2(transform.position.x + 0.3f, transform.position.y + 0.4f);
        Vector2 position3 = new Vector2(transform.position.x, transform.position.y + 0.3f);

        var effectObj = Instantiate(HitEffectPrefab, EffectTransform);
        effectObj.transform.position = position1;

        yield return new WaitForSeconds(0.1f);

        effectObj = Instantiate(HitEffectPrefab, EffectTransform);
        effectObj.transform.position = position2;

        yield return new WaitForSeconds(0.1f);

        effectObj = Instantiate(HitEffectPrefab, EffectTransform);
        effectObj.transform.position = position3;


        yield return null;
    }

    public void CheckResult()
    {
        if (Enemy.status.HP <= 0 || Player.status.HP <= 0)
        {
            // quit secuence
            StopAllCoroutines();
            battleSceneManager.SetClickPannelDisable(Turn.PLAYER);

            if (Enemy.status.HP <= 0)
            {
                battleSceneManager.BattleDescrition.text = "Player Win ";
            }
            else
            {
                battleSceneManager.BattleDescrition.text = "Player Lose";
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
        SoundManager.Instance.PlayBgm("MainBgm");
        if (Player.status.HP <= 0)
        {
            FindObjectOfType<Player>().LoadPlayer();
        }
        battleSceneManager.UnLoadScene();
    }
}
