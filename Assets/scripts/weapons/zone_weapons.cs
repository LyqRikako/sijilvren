using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone_weapons : weapon
{
    // Start is called before the first frame update
    public enemy_damager damager;
    // Start is called before the first frame update
    void Start()
    {
        SetStats();
        
    }

    // Update is called once per frame
    void Update()
    {
        //将holder进行旋转，xy不变，z进行旋转
        if (statsUpdated == true)
        {
            statsUpdated = false;
            SetStats();
        }
    }
    public void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.timeBetweenDamage = stats[weaponLevel].speed;
        damager.transform.localScale= Vector3.one*stats[weaponLevel].range;
        

    }
}
