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
    public float accuracyRate;
    public int healValue;
    public int manaCost;
    public string Effect;
    // for playing sound on each skill
    public string soundName;
    // for playing animation on each skill
    public GameObject EffectPrefab;
    // for skill type, Attack or Heal
    public SkillType type;
}
