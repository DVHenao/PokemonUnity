using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SkillType
{
    ATTACK,
    HEAL,
}

[CreateAssetMenu(fileName = "Skill", menuName = "Custom/Skill")]
public class Skill :ScriptableObject
{
    public string skillName;
    public int damageValue;
    public int healValue;
    public int manaCost;
    public string Effect;


    public SkillType type;

}
