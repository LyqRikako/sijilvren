using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//����ű�����һ����ʾ�˺����ֵ��ı���
public class damage_number : MonoBehaviour
{
    public TMP_Text damageText;
    public float lifeTime;
    private float lifeCounter;
    public float floatspeed = 1f;//�˺����ָ�����ʱ��
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

    //������ʾ����������
    public void setup(int damageDisplay)
    {
        lifeCounter = lifeTime;
        damageText.text = damageDisplay.ToString();
    }
}
