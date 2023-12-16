using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//ʹ��ui��
public class player_health_controller2 : MonoBehaviour
{
    public static player_health_controller2 instance;//ʵ����һ��controller����̬����������ʾ��unity��������

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
        //����uiѪ��
        health_slider.maxValue = maxHealth;
        health_slider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        /*//���ԣ�����t���󣬿�ʮ��Ѫ
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
           // Instantiate(deathEffect, transform.position, trasform.rotation)//gameObjectָ����ű����ӵ�inspetor��ֱ��ֹͣ�
        }
        health_slider.value = currentHealth;//��ÿ���ܵ��˺�ʱ���仯Ѫ����ui��ʾ
    }
}
