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
        //��holder������ת��xy���䣬z������ת
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
