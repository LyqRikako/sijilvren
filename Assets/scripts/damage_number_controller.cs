using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage_number_controller : MonoBehaviour
{
    //实例化一个伤害数字控制器，以供所有人调用
    public static damage_number_controller instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    public damage_number numberToSpawn;//文本框
    public Transform numberCanvas;//画布
    private List<damage_number> damageNumberPool=new List<damage_number>();//创建伤害显示数字的对象池
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.K))
        //{
        //    spawnDamage(55, new Vector3(1, 1, 1));
        //}
        
    }

    public void spawnDamage(float damageAmount,Vector3 location) 
    {
        int rounded = Mathf.RoundToInt(damageAmount);
        /*damage_number newDamage= Instantiate(numberToSpawn, location, Quaternion.identity, numberCanvas);*/
        damage_number newDamage = GetFromPool();
        newDamage.setup(rounded);
        newDamage.gameObject.SetActive(true);
        newDamage.transform.position = location;
    }

    public damage_number GetFromPool()
    {
        damage_number numberToOutPut = null;
        if(damageNumberPool.Count==0)
        {
            numberToOutPut = Instantiate(numberToSpawn, numberCanvas);
        }
        else
        {
            numberToOutPut = damageNumberPool[0];
            damageNumberPool.RemoveAt(0);
        }
        return numberToOutPut;
    }
    public void PlaceInToPool(damage_number numberToPlace) 
    {
        numberToPlace.gameObject.SetActive(false);
        damageNumberPool.Add(numberToPlace);
    }
}
