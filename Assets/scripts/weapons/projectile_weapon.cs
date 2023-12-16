using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_weapon : weapon
{
    public enemy_damager damager;
    public projectile projectile;
    private float shotCounter;
    public float weaponRange;
    public LayerMask whatIsEnemy;//��鷶Χ���Ƿ��е���
    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        //��holder������ת��xy���䣬z������ת
        if (statsUpdated == true)
        {
            statsUpdated = false;
            SetStats();
        }
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            shotCounter = stats[weaponLevel].timeBetweenAttacks;
            //��⸽���Ƿ��е���
            //OverlapCircleAll()����һ��Բ���Լ��Բ��������ײ��
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[weaponLevel].range, whatIsEnemy);
            if(enemies.Length > 0)
            {
                for (int i = 0; i < stats[weaponLevel].amount; i++)
                {
                    //����λ��
                    Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;
                    //���������תָ����ˣ�Rad2Deg:��radianceת��Ϊdegree
                    Vector3 direction = targetPosition - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    //�����Ƕ�
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
