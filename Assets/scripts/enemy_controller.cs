using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy_controller : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    private Transform target;
    public float damage;
    public float hitWaitTime=1f;//���ù������
    private float hitCounter;//����������
    public float health = 5f;
    public float knockBackTime = 0.5f;
    private float knockBackCounter;
    //�ڹ�������������õ��侭�飬�������Ը��ݹ��ﲻͬ���䲻ͬ�ľ���
    public exp_pickup exp;
    //public int expToGive = 1;
    public int coinvalue = 1;
    public float coinDropRate = .5f;
    // Start is called before the first frame update
    void Start()
    {
        target = player_health_controller.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;
            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }
            if (knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * 0.5f) ;
            }
        }
        theRB.velocity = (target.position - transform.position).normalized*moveSpeed;//�ƶ��ٶ���֡���޹�,ͨ�����������õ��ƶ��ٶȵ�ʸ��
        
        //���������ʱ������
        if (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //��unity�и�player������player��ǩ���ж���ײ�����������ײ�����Ѫ
        if (collision.gameObject.tag == "Player" && hitCounter <= 0f)//ע���Сд
        {
            //���ù��������ʱ������1��ʼ��������С�ڵ���0ʱ���Ż��ܵ�������ÿ�����ܹ���������Ϊ1
            player_health_controller.instance.TakeDamage(damage);
            hitCounter = hitWaitTime;
        }
        //��unity�и�player������player��ǩ���ж���ײ�����������ײ�����Ѫ
        if (collision.gameObject.tag == "Player2" && hitCounter <= 0f)//ע���Сд
        {
            //���ù��������ʱ������1��ʼ��������С�ڵ���0ʱ���Ż��ܵ�������ÿ�����ܹ���������Ϊ1
            player_health_controller2.instance.TakeDamage(damage);
            hitCounter = hitWaitTime;
        }
    }

    public void TakeDamage(float damageToTake) 
    {
        health -= damageToTake;
        //Vector3 newScale = transform.localScale;
        //newScale.y *= 0.5f;
        //transform.localScale = newScale;
        if (health <= 0)
        {
            SpawnExp(transform.position);
            Destroy(gameObject);
            //experience_level_controller.instance.SpawnExp(transform.position, expToGive);
            if (UnityEngine.Random.value <= coinDropRate)
            {
                coin_controller.instance.DropCoin(transform.position, coinvalue);
            }
        }
        damage_number_controller.instance.spawnDamage(damageToTake, transform.position);
    }
    //���ع����ܻ�����������һ������ģ��
    public void TakeDamage(float damageToTake,bool shouldKnockBack)
    {
        TakeDamage(damageToTake);
        if (shouldKnockBack == true)
        {
            knockBackCounter = knockBackTime;
        }
    }
    public void SpawnExp(Vector3 position)
    {
        Instantiate(exp, position, Quaternion.identity);
    }
}
