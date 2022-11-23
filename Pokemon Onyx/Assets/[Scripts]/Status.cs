using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Status
{
    [Header("Statsus")]
    public int HP;
    public int Mana;
    [Header("Skills")]
    public List<Skill> AttackSkillList;
    public List<Skill> CureSkillList;

}
