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
    public float hitWaitTime=1f;//设置攻击间隔
    private float hitCounter;//攻击计数器
    public float health = 5f;
    public float knockBackTime = 0.5f;
    private float knockBackCounter;
    //在怪物控制器里设置掉落经验，后续可以根据怪物不同掉落不同的经验
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
        theRB.velocity = (target.position - transform.position).normalized*moveSpeed;//移动速度与帧数无关,通过向量减法得到移动速度的矢量
        
        //攻击间隔计时器倒数
        if (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //在unity中给player打上了player标签，判断碰撞，如果发生碰撞，则掉血
        if (collision.gameObject.tag == "Player" && hitCounter <= 0f)//注意大小写
        {
            //设置攻击间隔计时器，从1开始倒数，当小于等于0时，才会受到攻击，每次遭受攻击后重置为1
            player_health_controller.instance.TakeDamage(damage);
            hitCounter = hitWaitTime;
        }
        //在unity中给player打上了player标签，判断碰撞，如果发生碰撞，则掉血
        if (collision.gameObject.tag == "Player2" && hitCounter <= 0f)//注意大小写
        {
            //设置攻击间隔计时器，从1开始倒数，当小于等于0时，才会受到攻击，每次遭受攻击后重置为1
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
    //重载怪物受击函数，增加一个击退模块
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
