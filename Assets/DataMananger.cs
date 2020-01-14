﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataMananger : MonoBehaviour
{
    public static DataMananger Instance = null;

    #region Data
    public Skill Data_Skills;
    public BotNameData Data_Bot;
    public List<Process_Player> Data_List_Player = new List<Process_Player>();
    public int CountPlayer = 4;
    public int Coin;
    [SerializeField ]int index = 0;
    [SerializeField] int index_1 = 0;
    #endregion


    #region Key

    public const string Key_Sound = "Key_Sound";
    public const string Key_Variable = "Key_Var";
    public const string Key_Shop = "Key_Shop";
    public const string Key_Coin = "Key_Coin";
    public const string Key_Name = "Key_Name";
    #endregion

    #region 

    public int IsMute = 1;
    public int IsVariable = 1;

    #endregion

    #region Transform
    public Text m_Coin;


    #endregion
    #region Option__Sound_Variable
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        GamePlayerCtrl.Instance.Event_Over_Game += Reset;
        Init_Key();
    }

    public void Init_Key()
    {
        if (!PlayerPrefs.HasKey(Key_Sound))
        {

            PlayerPrefs.SetInt(Key_Sound, 1);
            PlayerPrefs.Save();
        }
        else
        {
            IsMute = PlayerPrefs.GetInt(Key_Sound);
        }
        if (!PlayerPrefs.HasKey(Key_Variable))
        {

            PlayerPrefs.SetInt(Key_Variable, 1);
            PlayerPrefs.Save();
        }
        else
        {
            IsMute = PlayerPrefs.GetInt(Key_Sound);
        }

        //  INIT SHOP
      //   PlayerPrefs.DeleteKey(Key_Shop);
        if (!PlayerPrefs.HasKey(Key_Shop))
        {
          List<Infor_Skill> lists = new List<Infor_Skill>();

            for(int i = 0; i < Data_Skills.ListModel.Count; i++)
            {
                if (i != 0)
                {
                    Infor_Skill skill = new Infor_Skill(i, false, false,DataMananger.Instance.Data_Skills.Cost[i]);
                    lists.Add(skill);
                    

                }
                else
                {

                    Infor_Skill skill = new Infor_Skill(i, true, true, DataMananger.Instance.Data_Skills.Cost[i]);
                    lists.Add(skill);
                }
               
                    
            }


            List_Infor_Skill Save_list = new List_Infor_Skill(lists);
            string key =  JsonUtility.ToJson(Save_list);
            PlayerPrefs.SetString(Key_Shop, key);
            PlayerPrefs.Save();
            Data_Skills.List_infor_Skill = lists;
           
           List_Infor_Skill Save_list_1 = JsonUtility.FromJson<List_Infor_Skill>(PlayerPrefs.GetString(Key_Shop));
            Debug.Log("CO SKIN : " + Save_list_1.lists.Count);

        }
        else
        {
            List<Infor_Skill> list = JsonUtility.FromJson<List_Infor_Skill>(PlayerPrefs.GetString(Key_Shop)).lists;
            Load_To_ScriptableObject(list);
          



        }

        //////////
        //////////
        ///

        //Init Coin
        PlayerPrefs.DeleteKey(Key_Coin);
        if (!PlayerPrefs.HasKey(Key_Coin))
        {
            PlayerPrefs.SetInt(Key_Coin,999999);
            PlayerPrefs.Save();
            m_Coin.text = Get_Coin();
            Coin = PlayerPrefs.GetInt(Key_Coin);
        }
        else
        {
            Coin = PlayerPrefs.GetInt(Key_Coin);
            m_Coin.text = Get_Coin();

        }

    }
    public void Load_To_ScriptableObject(List<Infor_Skill> skills)
    {
        Data_Skills.List_infor_Skill = skills;

    }



    public int Is_Mute()
    {
        return PlayerPrefs.GetInt(Key_Sound);
    }
    public void Save_On_Off_Sound(bool mute)
    {
        if (mute)
        {
            PlayerPrefs.SetInt(Key_Sound, 1);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt(Key_Sound, 0);
            PlayerPrefs.Save();
        }
        
    }
    public int Is_Variable()
    {
        return PlayerPrefs.GetInt(Key_Variable);
    }

    public void Save_On_Off_Variable(bool mute)
    {
        if (mute)
        {
            PlayerPrefs.SetInt(Key_Variable, 1);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt(Key_Variable, 0);
            PlayerPrefs.Save();
        }

    }
    #endregion

    public void Save_Coin(int coin)
    {
        PlayerPrefs.SetInt(Key_Coin, coin);
        PlayerPrefs.Save();
    }
    public string Get_Coin()
    {
      return  PlayerPrefs.GetInt(Key_Coin).ToString();
       
    }
    public int Get_Coin_Current()
    {
        return PlayerPrefs.GetInt(Key_Coin);

    }
    public void Add_Coin(int coin)
    {
        this.Coin += coin;
        Save_Coin(this.Coin);
        m_Coin.text = Get_Coin();
       
    }
    public void Earn_Coin(int coin)
    {
        this.Coin -= coin;
        Save_Coin(this.Coin);
        m_Coin.text = this.Coin.ToString();
    }
    public void Set_Coin(int coin)
    {
        Save_Coin(coin);
    }
    public void Save_Shop(List<Infor_Skill> skills)
    {
        string key = JsonUtility.ToJson(skills);
        PlayerPrefs.SetString(Key_Shop, key);
        PlayerPrefs.Save();
        
    } 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void Reset()
    {
        index_1 = 0;
        index = 0;
        Data_List_Player.Clear();
    }
    public void Push_Data(List<Process_Player> processes)
    {
        Data_List_Player = new List<Process_Player>();
        Data_List_Player = processes;
    }

   
    public Process_Player Set_Random_Infor()
    {
        Process_Player process_Player = new Process_Player();
        var a = Data_Bot.botNames[Random.Range(0, Data_Bot.botNames.Count)];

        var b = a.botName[Random.Range(0, a.botName.Count)];

       process_Player.sprite = a.icon;
       process_Player.name = b;
        return process_Player;
    }
    public void Push_Infor(GetInfor player)
    {
        Process_Player process = Data_List_Player[index];
       
        
          
            player.Flag.sprite = process.sprite;
            player.Name.text = process.name;

        index++;
        Debug.Log("index : " + process.name);

    }
    public void Push_Infor(InforPlayer player)
    {
        Process_Player process = Data_List_Player[index_1];



        player.Sprite_Flag = process.sprite;
        player.namePlayer = process.name;
        
        index_1++;
      

    }


}
