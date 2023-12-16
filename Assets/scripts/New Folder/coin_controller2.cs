using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class coin_controller2 : MonoBehaviour
{
    public static coin_controller2 instance;
    private void Awake()
    {
        instance = this;
    }
    public int currentCoins;

    public CoinPickup2 coin;

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        ui_controller2.instance.UpdateCoins();
    }
    public void DropCoin(Vector3 position, int value)
    {
        CoinPickup2 newCoin = Instantiate(coin, position + new Vector3(.2f, .1f, 0f), Quaternion.identity);
        newCoin.coinAmount = value;
        newCoin.gameObject.SetActive(true);
    }
    public void SpendCoins(int coinToSpeed)
    {
        currentCoins -= coinToSpeed;
        ui_controller2.instance.UpdateCoins();
    }
        
}
