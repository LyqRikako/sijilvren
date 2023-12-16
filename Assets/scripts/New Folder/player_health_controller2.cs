using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//使用ui库
public class player_health_controller2 : MonoBehaviour
{
    public static player_health_controller2 instance;//实例化一个controller，静态变量不会显示在unity控制器中

    private void Awake()
    {
        instance = this;
    }
    public float currentHealth, maxHealth;
    public Slider health_slider;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //设置ui血条
        health_slider.maxValue = maxHealth;
        health_slider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        /*//测试，按下t键后，扣十滴血
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10f);
        }*/
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Level_Manager2.instance.Endlevel();
           // Instantiate(deathEffect, transform.position, trasform.rotation)//gameObject指这个脚本连接的inspetor，直接停止活动
        }
        health_slider.value = currentHealth;//在每次受到伤害时，变化血条的ui显示
    }
}
