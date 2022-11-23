using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObj", menuName = "ScriptableObjects/Monster", order = 3)]
public class Monster : ScriptableObject
{
    public MonsterBase monsterBaseVar;
    public int level;





    public int Attack
    {
        get { return Mathf.FloorToInt((monsterBaseVar.Attack * level) / 100f) + 5; }
    }

    public int Special
    {
        get { return Mathf.FloorToInt((monsterBaseVar.Special * level) / 100f) + 5; }
    }

    public int Speed
    {
        get { return Mathf.FloorToInt((monsterBaseVar.Speed * level) / 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((monsterBaseVar.Defense * level) / 100f) + 5; }
    }

    public int MaxHP
    {
        get { return Mathf.FloorToInt((monsterBaseVar.Health * level) / 100f) + 10; }
    }
}