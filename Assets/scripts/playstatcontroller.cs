using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playstatcontroller : MonoBehaviour
{
    public static playstatcontroller instance;

    private void Awake()
    {
        instance = this;
    }
    public List<playstatvalue> moveSpeed, health, pickupRange, maxWeapons;
    public int moveSpeedLevelCount, healthLevelCount, pickupRangeLevelCount;
    public int moveSpeedLevel, healthLevel, pickupRangeLevel, maxWeaponsLevel;
    void Start()
    {
        for (int i = moveSpeed.Count - 1; i < moveSpeedLevelCount; i++)
        {
            moveSpeed.Add(new playstatvalue(moveSpeed[i].cost + moveSpeed[1].cost, moveSpeed[i].value + (moveSpeed[1].value - moveSpeed[0].value)));
        }
        for (int i = health.Count - 1; i < healthLevelCount; i++)
        {
            health.Add(new playstatvalue(health[i].cost + health[1].cost, health[i].value + (health[1].value - health[0].value)));
        }
        for (int i = pickupRange.Count - 1; i < pickupRangeLevelCount; i++)
        {
            pickupRange.Add(new playstatvalue(pickupRange[i].cost + pickupRange[1].cost, pickupRange[i].value + (pickupRange[1].value - pickupRange[0].value)));
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (ui_controller.instance.levelUpPanel.activeSelf == true)
        {
            UpdateDisplay();
        }
    }
    public void UpdateDisplay()
    {
        if (moveSpeedLevel < moveSpeed.Count - 1)
        {
            ui_controller.instance.moveSpeedUpgradeDisplay.UpdateDisplay(moveSpeed[moveSpeedLevel + 1].cost, moveSpeed[moveSpeedLevel].value, moveSpeed[moveSpeedLevel + 1].value);
        }
        else
        {
            ui_controller.instance.moveSpeedUpgradeDisplay.ShowMaxLevel();
        }
        if (healthLevel < health.Count - 1)
        {
            ui_controller.instance.healthUpgradeDisplay.UpdateDisplay(health[healthLevel + 1].cost, health[healthLevel].value, health[healthLevel + 1].value);
        }
        else
        {
            ui_controller.instance.healthUpgradeDisplay.ShowMaxLevel();
        }
        if (pickupRangeLevel < pickupRange.Count - 1)
        {
            ui_controller.instance.pickupRangeUpgradeDisplay.UpdateDisplay(pickupRange[pickupRangeLevel + 1].cost, pickupRange[pickupRangeLevel].value, pickupRange[pickupRangeLevel + 1].value);
        }
        else
        {
            ui_controller.instance.pickupRangeUpgradeDisplay.ShowMaxLevel();
        }
        if (maxWeaponsLevel < maxWeapons.Count - 1)
        {
            ui_controller.instance.maxWeaponsUpgradeDisplay.UpdateDisplay(maxWeapons[maxWeaponsLevel + 1].cost, maxWeapons[maxWeaponsLevel].value, maxWeapons[maxWeaponsLevel + 1].value);
        }
        else
        {
            ui_controller.instance.maxWeaponsUpgradeDisplay.ShowMaxLevel();
        }
    }
    public void PurchaseMoveSpeed()
    {
        moveSpeedLevel++;
        coin_controller.instance.SpendCoins(moveSpeed[moveSpeedLevel].cost);
        UpdateDisplay();
        player_controller.instance.moveSpeed = moveSpeed[moveSpeedLevel].value;
    
    }
    public void PurchaseHealth()
    {
        healthLevel++;
        coin_controller.instance.SpendCoins(health[healthLevel].cost);
        UpdateDisplay();
        player_controller.instance.maxHealth = health[healthLevel].value;
        player_controller.instance.currentHealth = health[healthLevel].value - health[healthLevel - 1].value;
    }
    public void PurchasePickupRange()
    {
        pickupRangeLevel++;
        coin_controller.instance.SpendCoins(pickupRange[pickupRangeLevel].cost);
        UpdateDisplay();
        player_controller.instance.pickupRange = pickupRange[pickupRangeLevel].value;
    }
    public void PurchaseMaxWeapons()
    {
        maxWeaponsLevel++;
        coin_controller.instance.SpendCoins(maxWeapons[maxWeaponsLevel].cost);
        UpdateDisplay();
        player_controller.instance.maxWeapons = Mathf.RoundToInt(maxWeapons[maxWeaponsLevel].value);
    }
    [System.Serializable]
    public class playstatvalue
    {
        public int cost;
        public float value;
        public playstatvalue(int newCost, float newValue)
        {
            cost = newCost;
            value = newValue;
        
        }
    }
}
