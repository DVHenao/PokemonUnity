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

    public IEnumerator StartBattleSequenc(SkillType skillType)
    {
        battleSceneManager.SetClickPannelDisable(CurrentTurn);
        // animation
        yield return new WaitForSeconds(1.0f);
        PlayEffectAnimation(CurrentTurn, skillType);
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

    public void PlayEffectAnimation(Turn currentTurn, SkillType skillType)
    {
        Transform transformVal = null;
        GameObject selectedEffect = null;
        string soundname = "";
        switch (skillType)
        {
            case SkillType.ATTACK:
                transformVal = currentTurn == Turn.PLAYER ? Enemy.transform : Player.transform;
                break;
            case SkillType.HEAL:
                transformVal = currentTurn == Turn.PLAYER ? Player.transform : Enemy.transform;
                break;

        }
        selectedEffect = currentTurn == Turn.PLAYER ? Player.status.SelectedEffectPrefab : Enemy.status.SelectedEffectPrefab;
        soundname = currentTurn == Turn.PLAYER ? Player.status.SelectedSoundName : Enemy.status.SelectedSoundName;
        StartCoroutine(AnimationSequence(transformVal, selectedEffect, soundname));
    }

    IEnumerator AnimationSequence(Transform transform, GameObject effectPrefab, string soundName)
    {
        if (effectPrefab != null)
        {
            Vector2 position1 = new Vector2(transform.position.x - 0.3f, transform.position.y + 0.4f);
            Vector2 position2 = new Vector2(transform.position.x + 0.3f, transform.position.y + 0.4f);
            Vector2 position3 = new Vector2(transform.position.x, transform.position.y + 0.3f);

            var effectObj = Instantiate(effectPrefab, EffectTransform);
            effectObj.transform.position = position1;
            SoundManager.Instance.PlayFX(soundName, 0.5f);

            yield return new WaitForSeconds(0.1f);

            effectObj = Instantiate(effectPrefab, EffectTransform);
            effectObj.transform.position = position2;
            SoundManager.Instance.PlayFX(soundName, 0.5f);

            yield return new WaitForSeconds(0.1f);

            effectObj = Instantiate(effectPrefab, EffectTransform);
            effectObj.transform.position = position3;
            SoundManager.Instance.PlayFX(soundName, 0.5f);

        }
        yield return null;
    }

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
            FindObjectOfType<Player>().LoadPlayer();
        }
        FindObjectOfType<PlayerController>().encounterActive = false;
        battleSceneManager.UnLoadScene();
    }
}
