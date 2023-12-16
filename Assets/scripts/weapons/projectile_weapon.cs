using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_weapon : weapon
{
    public enemy_damager damager;
    public projectile projectile;
    private float shotCounter;
    public float weaponRange;
    public LayerMask whatIsEnemy;//检查范围内是否有敌人
    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        //将holder进行旋转，xy不变，z进行旋转
        if (statsUpdated == true)
        {
            statsUpdated = false;
            SetStats();
        }
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            shotCounter = stats[weaponLevel].timeBetweenAttacks;
            //检测附近是否有敌人
            //OverlapCircleAll()创建一个圆用以检测圆内所有碰撞体
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[weaponLevel].range, whatIsEnemy);
            if(enemies.Length > 0)
            {
                for (int i = 0; i < stats[weaponLevel].amount; i++)
                {
                    //敌人位置
                    Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;
                    //弹丸如何旋转指向敌人，Rad2Deg:将radiance转换为degree
                    Vector3 direction = targetPosition - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    //修正角度
                    angle -= 90;
                    projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    
                    Instantiate(projectile,projectile.transform.position,projectile.transform.rotation).gameObject.SetActive(true);
                }
            }
        }
    }
    void SetStats() 
    {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        shotCounter = 0f;
        projectile.moveSpeed = stats[weaponLevel].speed;
    }
}
