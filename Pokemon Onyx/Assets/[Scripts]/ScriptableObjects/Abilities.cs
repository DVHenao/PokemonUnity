using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObj", menuName = "ScriptableObjects/Abilities", order = 2)]
public class Abilities : ScriptableObject
{
    public string abilityName;
    public Sprite image;
    public Type abilityType;

   

}