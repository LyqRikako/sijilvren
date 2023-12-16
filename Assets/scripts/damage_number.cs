using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//这个脚本控制一个显示伤害数字的文本框
public class damage_number : MonoBehaviour
{
    public TMP_Text damageText;
    public float lifeTime;
    private float lifeCounter;
    public float floatspeed = 1f;//伤害数字浮动的时间
    // Start is called before the first frame update
    void Start()
    {
        lifeCounter = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeCounter > 0) 
        {
            lifeCounter -= Time.deltaTime;
            if(lifeCounter<=0)
            {
                //Destroy(gameObject);
                damage_number_controller.instance.PlaceInToPool(this);
            }
        }

        transform.position += Vector3.up * floatspeed * Time.deltaTime;
    }

    //用于显示的文字内容
    public void setup(int damageDisplay)
    {
        lifeCounter = lifeTime;
        damageText.text = damageDisplay.ToString();
    }
}
