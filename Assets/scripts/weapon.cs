using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public List<WeaponStats> stats=new List<WeaponStats>();
    public int weaponLevel;
    [HideInInspector]
    public bool statsUpdated;
    public Sprite icon;//用来显示的图标

    public void LevelUp()
    {
        if (weaponLevel < stats.Count - 1)
        {
            weaponLevel++;
            statsUpdated = true;
            if (weaponLevel >= stats.Count - 1)
            {
                player_controller.instance.fullyLevelledweapons.Add(this);
                player_controller.instance.assignedWeapons.Remove(this);
            }
        }
    }
}

[System.Serializable]
public class WeaponStats
{
    public float speed, damage, range, timeBetweenAttacks, amount, duration;
    public string upgradeText;
}
