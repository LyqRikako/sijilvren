using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;


public class playerhealthcontroler : MonoBehaviour
{
    public static playerhealthcontroler instance;
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
        maxHealth = playstatcontroller.instance.health[0].value;
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
            Level_Manager.instance.Endlevel();

            Instantiate(deathEffect, transform.position, transform.rotation);
        }//��֪��Ϊʲô��Ч����������˵objectûֱ���������
        healthSlider.value = currentHealth;
    }
}
