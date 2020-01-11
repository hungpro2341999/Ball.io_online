﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Windown_Type {Start,Shop,Setting,Quest,Play,Revice,Game_Over,Rank,End_Game}
public enum Screen_Type {Screen_Start,Screen_Play,Screen_loading}

public class Mutiply_Screen : MonoBehaviour
{
    public Screen_Type Type_Screen;
    public List<Windown> Windows;
    //   public BotNameData botnameData;

    // Start is called before the first frame update
    private void Awake()
    {
        switch (Type_Screen)
        {
            case Screen_Type.Screen_Start:

                CloseAll(Windown_Type.Start);
                break;
            case Screen_Type.Screen_Play:

                CloseAll(Windown_Type.Play);
                break;
            case Screen_Type.Screen_loading:


                break;
        }
    }
    void Start()
    {
        
       
    }

    

    // Update is called once per frame
    public  void CloseAll(Windown_Type type)
    {
        foreach(Windown w in Windows)
        {
           if(w.type == type)
            {
                w.Open();
            }
            else
            {
                w.Close();
            }
        }
    }

   

    void Update()
    {


    }

    public void Open_Screen()
    {
        gameObject.SetActive(true);
      
    }
    public void Close_Screen()
    {
        gameObject.SetActive(false);
    }
    public void OpenWindow(Windown windown)
    {
        foreach(Windown w in Windows)
        {
            if(w.type == windown.type)
            {
                Debug.Log("Open");
                windown.Open();
            }
        }
    }
    public void OpenWindow(Windown_Type windown)
    {
        foreach (Windown w in Windows)
        {
            if (w.type == windown)
            {
                Debug.Log("Open");
                w.Open();
            }
        }
    }
    public void CloseWindow(Windown windown)
    {
        foreach (Windown w in Windows)
        {
            if (w.type == windown.type)
            {
                windown.Close();
            }
        }
    }
    public void CloseWindow(Windown_Type windown)
    {
        foreach (Windown w in Windows)
        {
            if (w.type == windown)
            {
                w.Close();
            }
        }
    }


}