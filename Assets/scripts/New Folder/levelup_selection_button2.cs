using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class levelup_selection_button2 : MonoBehaviour
{
    public TMP_Text upgradeDescText, nameLevelText;
    public Image weaponIcon;
    private weapon assignedWeapon;//升级时用来存储发来的武器数据
    public void UpdateButtonDisplay(weapon theWeapon) 
    {
        if (theWeapon.gameObject.activeSelf == true)
        {
            upgradeDescText.text = theWeapon.stats[theWeapon.weaponLevel].upgradeText;
            weaponIcon.sprite = theWeapon.icon;
            nameLevelText.text = theWeapon.name + " -Lvl " + theWeapon.weaponLevel;
           
        }
        else
        {
            upgradeDescText.text = "Unlock" + theWeapon.name;
            weaponIcon.sprite = theWeapon.icon;
            nameLevelText.text = theWeapon.name;
        }
        assignedWeapon = theWeapon;
    }
    public void SelectUpgrade()
    {
        if(assignedWeapon != null) 
        {
            if (assignedWeapon.gameObject.activeSelf == true)
            {
                assignedWeapon.LevelUp();
            }
            else 
            {
                player_controller2.instance.AddWeapon(assignedWeapon);
            }
            
            ui_controller2.instance.levelUpPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
