using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;


public class playerhealthcontroler2 : MonoBehaviour
{
    public static playerhealthcontroler2 instance;
    private void Awake()
    {
        instance = this;
    }
    public float currentHealth, maxHealth;
    public Slider healthSlider;
    public GameObject deathEffect;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = playstatcontroller2.instance.health[0].value;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Level_Manager2.instance.Endlevel();

            Instantiate(deathEffect, transform.position, transform.rotation);
        }//不知道为什么特效出不出来，说object没直到。。奇怪
        healthSlider.value = currentHealth;
    }
}
