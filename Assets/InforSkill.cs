﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InforSkill : MonoBehaviour,IPointerDownHandler
{
    public Skill skill;
    public Transform Parent_Skill;
    public Infor_Skill infor;
    public bool isChoice = false;
    public Transform choice;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Load_Infor()
    {
       if(infor.isBuy)
        {
           
        }
        if (infor.isUse)
        {
               
        }
    }
   public void Choice()
    {
        choice.gameObject.SetActive(true);
    }
    public void Un_Choice()
    {
        choice.gameObject.SetActive(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(infor.isBuy != true)
        {
            Shop_Mananger.Choice(infor.id);
            Shop_Mananger.Cost = infor.Cost.ToString();
        }
        else
        {
            Shop_Mananger.Choice(infor.id);
            Shop_Mananger.Cost = "USE";
        }
       
    }
}
