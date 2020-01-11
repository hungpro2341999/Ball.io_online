using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Type_Skill { TEXTTURE, MODEL }
[CreateAssetMenu(fileName ="New Skill",menuName ="Skill/Skill_1")]

public class Skill : ScriptableObject
{
    public int id;
    public Type_Skill Type_Skill;
    public Sprite ImgSkill;
    public Material Material;
    public GameObject Model;
    
}
