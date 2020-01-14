﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Type_Skill { TEXTTURE, MODEL }
[CreateAssetMenu(fileName ="New Skill",menuName ="Skill/Skill_1")]

public class Skill : ScriptableObject
{
    public List<GameObject> ListModel;
    public List<Infor_Skill> List_infor_Skill;
    public List<int> Cost;

  
    public bool isBuy = false;
    public bool isUse = false;
    public Type_Skill Type_Skill;
    public Sprite ImgSkill;
    public Material Material;
    public GameObject Model;
    public int SkillUse;
}
