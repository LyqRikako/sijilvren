using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class experience_level_controller : MonoBehaviour
{
    public static experience_level_controller instance;

    private void Awake()
    {
        instance = this;
    }
    public int currentExperience;
    //pubilc ExpPickup pickup;
    public List<int> expLevels;//每个等级需要的经验
    public int currentLevel=1,levelCount=50;
    public List<weapon> weaponsToUpgrade;

    // Start is called before the first frame update
    void Start()
    {

        while (expLevels.Count < levelCount) 
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetExp(int amountToGet) 
    {
        currentExperience += amountToGet;
        if (currentExperience >= expLevels[currentLevel])
        {
            LevelUp();
        }
        ui_controller.instance.UpdateExperience(currentExperience, expLevels[currentLevel], currentLevel);
    }

    void LevelUp() 
    {
        currentExperience -= expLevels[currentLevel];
        currentLevel++;
        if(currentLevel>=expLevels.Count)
        {
            currentLevel = expLevels.Count - 1;
        }
        //player_controller.instance.activeWeapon.LevelUp();
        ui_controller.instance.levelUpPanel.SetActive(true);
        Time.timeScale = 0f;
        //ui_controller.instance.levelUpButtons[1].UpdateButtonDisplay(player_controller.instance.activWeapon);
        //ui_controller.instance.levelUpButtons[0].UpdateButtonDisplay(player_controller.instance.assignedWeapons[0]);
        //ui_controller.instance.levelUpButtons[1].UpdateButtonDisplay(player_controller.instance.unassignedWeapons[0]);
        //ui_controller.instance.levelUpButtons[2].UpdateButtonDisplay(player_controller.instance.unassignedWeapons[1]);
       
        weaponsToUpgrade.Clear();

        List<weapon> availableweapons = new List<weapon>();
        availableweapons.AddRange(player_controller.instance.assignedWeapons);

        if (availableweapons.Count > 0)
        {
            int selected = Random.Range(0, availableweapons.Count);
            weaponsToUpgrade.Add(availableweapons[selected]);
            availableweapons.RemoveAt(selected);
        }
        if (player_controller.instance.assignedWeapons.Count + player_controller.instance.fullyLevelledweapons.Count < player_controller.instance.maxWeapons)
        {
            availableweapons.AddRange(player_controller.instance.unassignedWeapons);
        }

        for (int i = weaponsToUpgrade.Count; i < 3; i++)
        {
            if (availableweapons.Count > 0)
            {
                int selected = Random.Range(0, availableweapons.Count);
                weaponsToUpgrade.Add(availableweapons[selected]);
                availableweapons.RemoveAt(selected);
            }
        }

        for (int i = 0; i < weaponsToUpgrade.Count; i++)
        {
            ui_controller.instance.levelUpButtons[i].UpdateButtonDisplay(weaponsToUpgrade[i]);
        }

        for (int i = 0; i < ui_controller.instance.levelUpButtons.Length; i++)
        {
            if (i < weaponsToUpgrade.Count)
            {
                ui_controller.instance.levelUpButtons[i].gameObject.SetActive(true);
            }
            else
            { 
                ui_controller.instance.levelUpButtons[i].gameObject.SetActive(false);

            }
        }
        playstatcontroller.instance.UpdateDisplay();
    }
}
