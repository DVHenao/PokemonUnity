using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Status
{
    [Header("Statsus")]
    public int HP;
    public int MaxHP;
    public int Mana;
    public int MaxMana;
    [Header("Skills")]
    public List<Skill> AttackSkillList;
    public List<Skill> CureSkillList;

    public GameObject SelectedEffectPrefab = null;
    public string SelectedSoundName = "";


    public void Heal(int val)
    {
        HP += val;
        if (HP > MaxHP)
        {
            HP = MaxHP;
        }
    }

    public void ChaargeMana(int val)
    {
        Mana += val;
        if (Mana > MaxMana)
        {
            Mana = MaxMana;
        }
    }

    public void Damaged(int damage)
    {
        HP -= damage;
        if (HP < 0)
        {
            HP = 0;
        }
    }

    public void UseMana(int mana)
    {
        Mana -= mana;
        if (Mana < 0)
        {
            Mana = 0;
        }

    }
}
