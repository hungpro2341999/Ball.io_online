﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InforPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite Sprite_Flag;
    public string namePlayer ="";
   
    void Start()
    {
     
    }

    // Update is called once per frame
    public void SetInfor()
    {
       if( gameObject.tag != "Player")
        {
            DataMananger.Instance.Push_Infor(this);
        }
      
       
    }
    void Update()
    {
      
    }
}
