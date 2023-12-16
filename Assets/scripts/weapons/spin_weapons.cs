using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class spin_weapons : weapon
{
    public float rotateSpeed;
    public Transform holder;
    public enemy_damager damager;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        SetStats();
        //ui_controller.instance.levelUpButtons[0].UpdateButtonDisplay(this);
    }

    // Update is called once per frame
    void Update()
    {
        //将holder进行旋转，xy不变，z进行旋转
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime*speed));
        if (statsUpdated == true) 
        {
            statsUpdated = false;
            SetStats();
        }
    }
    public void SetStats() 
    {
        damager.damageAmount = stats[weaponLevel].damage;
        transform.localScale = Vector3.one * stats[weaponLevel].range;
        speed= stats[weaponLevel].speed;
    }
}
