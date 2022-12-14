using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController
{
    private GameObject selfObject;
    private Enemy self;

    public EnemyAIController(GameObject selfObject)
    {
        this.selfObject = selfObject;
        self = selfObject.GetComponent<Enemy>();
    }


    // Simple Enemy AI
    public void ExcuteAI()
    {
        switch (self.currentState)
        {
            case EnemyState.NORMAL:
                Attack();
                break;

            case EnemyState.LOW_HEALTH:
                if(!Heal())
                {
                    Attack();
                }
                break;
        }
    }

    private bool Attack()
    {
        List<Skill> attackSkillList = self.status.AttackSkillList;

        // get skills under mana character get.
        List<Skill> selectedSkills = attackSkillList.FindAll(skill => skill.manaCost <= self.status.Mana);

        int randNum = Random.Range(0, selectedSkills.Count);
        Skill skillPicked = selectedSkills[randNum];

        self.gameObject.GetComponentInParent<Animator>().Play("MonsterShaking", 0, 0.0f);
        self.battleManager.battleSceneManager.BattleDescrition.color = Color.magenta;
        self.battleManager.battleSceneManager.BattleDescrition.text = self.Name + " " + skillPicked.Effect;

        // for playing VFX
        self.status.SelectedEffectPrefab = skillPicked.EffectPrefab;
        // for playing SFX
        self.status.SelectedSoundName = skillPicked.soundName;
        self.status.UseMana(skillPicked.manaCost);
        self.battleManager.Player.status.Damaged(skillPicked.damageValue);
        self.battleManager.StartCoroutine(self.battleManager.StartBattleSequenc(skillPicked.type));

        return true;
    }

    private bool Heal()
    {
        List<Skill> cureSkillList = self.status.CureSkillList;

        // get skills under mana character get.
        List<Skill> selectedSkills = cureSkillList.FindAll(skill => skill.manaCost <= self.status.Mana);
        if (selectedSkills.Count <= 0)
            return false;

        int randNum = Random.Range(0, selectedSkills.Count);
        Skill skillPicked = selectedSkills[randNum];

        self.battleManager.battleSceneManager.BattleDescrition.color = Color.magenta;
        self.battleManager.battleSceneManager.BattleDescrition.text = self.Name + " " + skillPicked.Effect;

        // for playing VFX
        self.status.SelectedEffectPrefab = skillPicked.EffectPrefab;
        // for playing SFX
        self.status.SelectedSoundName = skillPicked.soundName;
        self.status.UseMana(skillPicked.manaCost);
        self.status.Heal(skillPicked.healValue);
        self.battleManager.StartCoroutine(self.battleManager.StartBattleSequenc(skillPicked.type));
        return true;
    }
}
