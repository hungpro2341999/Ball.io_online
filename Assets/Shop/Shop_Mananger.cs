using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Mananger : MonoBehaviour
{
    public GameObject Skill;
    public Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        LoadShop(DataMananger.Instance.Data_Skills);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LoadShop(List<Skill> skills)
    {
        for(int i = 0; i < skills.Count; i++)
        {
            Instantiate(Skill, parent);
        }
    }
}
