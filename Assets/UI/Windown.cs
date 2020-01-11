﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windown : MonoBehaviour
{
    
    public Windown_Type type;
    // Start is called before the first frame update
    void Start()
    {
      
    }
    IEnumerator BackToStartScreen()
    {

        yield return new WaitForSeconds(3);
        GamePlayerCtrl.Instance.Event_Over_Game();
        GameMangaer.Instance.Open_Screen(Screen_Type.Screen_Start);
        Close();
        Debug.Log("Back To Screen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Open()
    {
        gameObject.SetActive(true);
        if (type == Windown_Type.End_Game)
        {
            StartCoroutine(BackToStartScreen());
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
