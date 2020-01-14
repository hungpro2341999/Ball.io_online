using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Mananger : MonoBehaviour
{
    public static int Stuff_Choice=-1;
    public static Shop_Mananger Instance = null;
    public GameObject Skill;
    public Transform parent;
    public static string Cost ="";
    public Text Text_Cost;
    public  List<GameObject> listSkill;
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
    }
    // Start is called before the first frame update
    void Start()
    {
        Load_Shop();
    }

    // Update is called once per frame
    void Update()
    {
        Text_Cost.text = Shop_Mananger.Cost;
    }
    public void Load_Shop()
    {
        int count = DataMananger.Instance.Data_Skills.ListModel.Count;
        Debug.Log("LOAD : " + count);
        for(int i = 0; i < count; i++)
        {
           var a = Instantiate(Skill, parent);
            a.GetComponent<InforSkill>().infor  = DataMananger.Instance.Data_Skills.List_infor_Skill[i];
            a.GetComponent<InforSkill>().Load_Infor();
           listSkill.Add(a);
        }
    }
    public void SetCode(string cost)
    {
        Shop_Mananger.Cost = cost;
    }
    public static  void Choice(int id)
    {
        Stuff_Choice = id;
      for(int i = 0; i < Shop_Mananger.Instance.listSkill.Count; i++)
        {
            if (Shop_Mananger.Instance.listSkill[i].GetComponent<InforSkill>().infor.id == id)
            {
                Shop_Mananger.Instance.listSkill[i].GetComponent<InforSkill>().Choice();
            }
            else
            {
                Shop_Mananger.Instance.listSkill[i].GetComponent<InforSkill>().Un_Choice();
            }
        }
    }
    public void Buy()
    {
        if (Text_Cost.text!="BUYED")
        {
         

            if (Stuff_Choice != -1)
            {
                    for (int i = 0; i < listSkill.Count; i++)
                {
                    InforSkill infor =  Shop_Mananger.Instance.listSkill[i].GetComponent<InforSkill>();
                    if (Stuff_Choice == infor.infor.id)
                    {
                        if (!infor.infor.isBuy)
                        {
                            if (infor.infor.Cost <= DataMananger.Instance.Get_Coin_Current())
                            {
                                DataMananger.Instance.Earn_Coin(infor.infor.Cost);
                                infor.infor.isBuy = true;
                                SetCode("Buy");

                            }

                        }
                    }
                   
                }                
            }
        }
        else
        {

        }
        Load_Infor();
    }
    public void Load_Infor()
    {
        List<Infor_Skill> lists = new List<Infor_Skill>();
        
        for(int i=0;i< Shop_Mananger.Instance.listSkill.Count; i++)
        {
            lists.Add(Shop_Mananger.Instance.listSkill[i].GetComponent<InforSkill>().infor);    
        }
        DataMananger.Instance.Save_Shop(lists);
    }
}
