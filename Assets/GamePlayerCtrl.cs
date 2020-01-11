using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusGame { Play,Pause,Out_Game,Wait,Reset,Game_Over,Watching,End_Game,None}
public class GamePlayerCtrl : MonoBehaviour
{
    public static GamePlayerCtrl Instance;
    public Mutiply_Screen Windown;
    public VisibleInforPlayer visible;
    public StatusGame Status = StatusGame.Wait;
    public GameObject AI;
    public GameObject Player;
    public Player[] player;
    public Player Main_Player;
    public float Radius;
    public float OffSet;
    public float Ground;
    public float Count_Player = 5;
    public Transform Parent;
    public bool isGameOver = false;
    public bool isGamePause = false;
    public bool isWinGame = false;
    public delegate void Event_Game();
    public LayerMask MaskPlayer;
    public Event_Game Event_Reset_Game;
   
    public Event_Game Event_Over_Game;
    public Event_Game Event_Back_To_Screen_Game;
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

        Event_Reset_Game += Start_Game;
        Event_Over_Game += GameOver;
    }

    public void GameOver()
    {
        DestroyAll();
        player = null;
        Main_Player = null;
    }
    

    public int CountPlayer()
    {
        return player.Length;
    }

    // Start is called before the first frame update
    void Start()
    {
        
      
    }
    void Update()
    {

        
       
    }

    public void EndGame()
    {
        isGameOver = true;
        isGamePause = true;
        Status = StatusGame.None;
        for(int i = 0; i < player.Length; i++)
        {
            player[i].GetComponent<Enemy>().Destroy();
        }
    }
    private void FixedUpdate()
    {
        if(Status == StatusGame.Out_Game)
        {
            if (Is_End_Game())
            {
                Status = StatusGame.End_Game;
                Debug.Log("END_GAME");
                Windown.CloseWindow(Windown_Type.End_Game);
            }
        }

        if (Status == StatusGame.Play)
        {
            if (Is_Game_OVer())
            {
                Status = StatusGame.Out_Game;
                Debug.Log("GameOVer");
                Windown.OpenWindow(Windown_Type.Game_Over);

            }
            if (Is_End_Game())
            {
                Status = StatusGame.End_Game;
                Debug.Log("END_GAME");
                Windown.CloseWindow(Windown_Type.End_Game);
            }
        }
        if (Status == StatusGame.Watching)
        {
            if (isGameOver)
            {
                Windown.CloseWindow(Windown_Type.Game_Over);
            }
            Status = StatusGame.Out_Game;
        }
        if (Status == StatusGame.End_Game)
        {
            Windown.OpenWindow(Windown_Type.End_Game);
            Status = StatusGame.None;

        }
    }

    public void Start_Game()
    {

       
        InforMap inforMap = GameObject.Find("Map").GetComponent<InforMap>();
        Radius = inforMap.Radius;
        Ground = inforMap.Ground;
        Count_Player = DataMananger.Instance.CountPlayer;
        Reset_Game();
        StartCoroutine(Wait_Start_Game());

    }
    public IEnumerator Wait_Start_Game()
    {
        Status = StatusGame.None;
        isGamePause = true;
        isGameOver = true;
        for(int i = 0; i < player.Length; i++)
        {
            player[i].GetComponent<Rigidbody>().isKinematic = true;
        }
        yield return new WaitForSeconds(3);
        for (int i = 0; i < player.Length; i++)
        {
            player[i].GetComponent<Rigidbody>().isKinematic = false;
        }
        Status = StatusGame.Play;
        isGameOver = false;
        isGamePause = false;
    }
    public bool Is_Game_OVer()
    {
        if (Main_Player != null)
        {
            if (!Main_Player.GetComponent<Enemy>().isGround)
            {
                isGameOver = true;

            }
            else
            {
                isGameOver = false;
            }
            return isGameOver;
        }
       return false;
       
    }

    public bool Is_End_Game()
    {
        if (player != null)
        {
            bool isWin = false;
            int count = 0;
            for (int i = 0; i < player.Length; i++)
            {
                if (player[i].GetComponent<Enemy>().isGround)
                {
                    count++;
                }
            }
            if (count == 1)
            {
                isWin = true;
            }

            return isWin;
        }
        else
        {
            return true;
        }
       
    }
   

    public void InitGame()
    {
        
        player = GameObject.FindObjectsOfType<Player>();
        Debug.Log("COUNT :" + player.Length);
        for (int i = 0; i < Count_Player; i++)
        {
            if(player[i].tag == "Player")
            {
                Main_Player = player[i];
               
                break;
            }

        }
    }

    // Update is called once per frame
    

    public Player getEneMyNearst(Player player)
    {
        if (this.player.Length > 1)
        {
            Player target = null;
            Vector3 pos = player.transform.position;
            float distance = Mathf.Infinity;
            for (int i = 0; i < this.player.Length; i++)
            {
                if (this.player[i].gameObject.name != player.gameObject.name)
                {
                    if (Vector3.Distance(pos, this.player[i].transform.position) < distance)
                    {
                        distance = Vector3.Distance(pos, this.player[i].transform.position);

                        target = this.player[i];
                    }
                }

            }
            //   Debug.Log(player.name + ": Target : " + target.name);

         //   Debug.DrawLine(player.transform.position, target.transform.position, Color.red);
            return target;
        }
        else
        {
            Debug.Log("You Alone");
            return null;
        }

    }
    
   

    public void PushInfor()
    {

        
    }




    public void RemovePlayer(Player player)
    {

    }

    public Player getPlayer(int index)
    {
        return this.player[index];
    }
    public void DestroyAll()
    {
       Enemy[] player = GameObject.FindObjectsOfType<Enemy>();
        for(int i = 0; i < player.Length; i++)
        {
            Debug.Log("NAME : " +player[i].name);
            player[i].GetComponent<Enemy>().Destroy();
            player[i].gameObject.SetActive(false);
        }
        
            
        

    }
    public void Reset_Game()
    {
      
        DestroyAll();
        for(int i=0; i<Count_Player; i++)
        {
            bool Accpect = false;
            while (!Accpect)
            {
                Vector3 pos = Random.insideUnitCircle * Radius;
                pos = new Vector3(pos.x, Ground + OffSet, pos.y);
                if (Physics.SphereCast(new Ray(pos,transform.up),0.75f, 0, MaskPlayer))
                {
                   Accpect = false;
                }
                else
                {
                    var a = Instantiate(AI, pos, Quaternion.identity, Parent);
                    a.name = "AI_" + i;
                    a.GetComponent<InforPlayer>().SetInfor();
                    Accpect = true;
                   
                }
            }
           
           
         
            
            
        }
        bool Accpect1 = false;
        while (!Accpect1)
        {
            Vector3 pos = Random.insideUnitCircle * Radius;
            pos = new Vector3(pos.x, Ground + OffSet, pos.y);
            if (Physics.SphereCast(new Ray(pos, transform.up), 0.65f, 0, LayerMask.NameToLayer("Enemy")))
            {
                Accpect1 = false;
            }
            else
            {
                Accpect1 = true;
                var a1 = Instantiate(Player, pos, Quaternion.identity, Parent);
                a1.name = "Player";
                a1.GetComponent<InforPlayer>().SetInfor();
                Accpect1 = true;
                InitGame();
                visible.Init();
            }
        }
       
     
       

    }

    public void Claim()
    {
        Status = StatusGame.Watching;
    }
    

    
  
    


}
