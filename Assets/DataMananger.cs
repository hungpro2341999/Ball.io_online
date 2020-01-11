using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataMananger : MonoBehaviour
{
    public static DataMananger Instance = null;

    #region Data
    public List<Skill> Data_Skills;
    public BotNameData Data_Bot;
    public List<Process_Player> Data_List_Player = new List<Process_Player>();
    public int CountPlayer = 4;
    [SerializeField ]int index = 0;
    [SerializeField] int index_1 = 0;
    #endregion


    #region Key

    public const string Key_Sound = "Key_Sound";
    public const string Key_Variable = "Key_Var";

    #endregion

    #region 

    public int IsMute = 1;
    public int IsVariable = 1;

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
