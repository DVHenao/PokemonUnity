using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObj", menuName = "ScriptableObjects/MonsterBase", order = 3)]
public class MonsterBase : ScriptableObject
{
    public string monsterName;
    public Type monsterType;

    public List<Abilities> abilities;

    public Sprite frontSprite;
    public Sprite backSprite;


    // attributes below
    public int health;
    public int attack;
    public int special;
    public int speed;
    public int defense;


    public string Name
    {
        get { return monsterName; }
    }
    public int Health
    {
        get { return health; }
    }
    public int Attack
    {
        get { return attack; }
    }
    public int Special
    {
        get { return special; }
    }
    public int Speed
    {
        get { return speed; }
    }
    public int Defense
    {
        get { return defense; }
    }
    

}

public enum Type
{
    Normal,
    Fire,
    Water,
    Thunder,
    Earth,
    Dark,
    Light
}