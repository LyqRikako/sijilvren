using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy_spawner : MonoBehaviour
{
    public GameObject enemyToSpawn;//做一个生成怪物的模块,这个模块调用的是写好的slime模板
    public float timeToSpawn;
    private float spawmCounter;//生成怪物的计时器
    public Transform minSpawn, maxSpawn;
    private Transform target;
    private float despawnDistance;//怪物解体的最远距离，超过这个距离，小怪会解体
    private List<GameObject> spawnEnemies = new List<GameObject>();
    public int checkPerFrame;//每帧要检查的敌人数量
    private int enemyToCheck;//查看的标记点

    public List<waveInfo> waves;//一波一波的生成敌人
    private int currentWave;//当前波数
    private float waveCounter;//时间倒计时
    // Start is called before the first frame update
    void Start()
    {
        //spawmCounter = timeToSpawn;
        target = player_health_controller.instance.transform;
        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 6f;//比刷怪点多6个距离
        currentWave = -1;
    }

    // Update is called once per frame
    void Update()
    {
        //以下是原本用于生成怪物的方法，之后采用一波一波刷怪
        /*spawmCounter -= Time.deltaTime;
        if (spawmCounter<=0)
        {
            spawmCounter = timeToSpawn;
            GameObject newEnemy= Instantiate(enemyToSpawn, selectSpawnPoint(), transform.rotation);
            spawnEnemies.Add(newEnemy);//每次生成一个怪物后，将他加入我们的列表
        }*/
        if (player_health_controller.instance.gameObject.activeSelf)
        {
            if(currentWave<waves.Count)
            {
                waveCounter -= Time.deltaTime;
                if(waveCounter<=0) 
                {
                    GoToNextWave();
                }
                spawmCounter-= Time.deltaTime;
                if (spawmCounter <= 0) 
                {
                    spawmCounter = waves[currentWave].timeBetweenSpawns;
                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, selectSpawnPoint(), transform.rotation);
                    spawnEnemies.Add(newEnemy);//每次生成一个怪物后，将他加入我们的列表
                }
            }
        }
        transform.position = target.position;

        int checkTarget = enemyToCheck + checkPerFrame;
        while (enemyToCheck < checkTarget)
        {
            if (enemyToCheck < spawnEnemies.Count)
            {
                if (spawnEnemies[enemyToCheck] != null)
                {
                    if (Vector3.Distance(transform.position, spawnEnemies[enemyToCheck].transform.position) > despawnDistance)
                    {
                        Destroy(spawnEnemies[enemyToCheck]);
                        spawnEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    }
                    else
                    {
                        enemyToCheck++;
                    }
                }
                else 
                {
                    spawnEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            }
            else 
            {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }

    public Vector3 selectSpawnPoint() 
    {

        Vector3 spawnPoint = Vector3.zero;
        bool spawnVerticalEdge = Random.Range(0f, 1f) > 0.5f;
        if(spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);
            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPoint.x = maxSpawn.position.x;
            }
            else 
            {
                spawnPoint.x = minSpawn.position.x;
            }
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);
            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPoint.y = maxSpawn.position.x;
            }
            else
            {
                spawnPoint.y = minSpawn.position.x;
            }
        }
        return spawnPoint;
    }

    public void GoToNextWave() 
    {
        currentWave++;
        if(currentWave>=waves.Count)
        {
            currentWave = waves.Count - 1;
        }
        waveCounter = waves[currentWave].waveLength;
        spawmCounter = waves[currentWave].timeBetweenSpawns;
    }
}

[System.Serializable]//序列化，在unity中可以显示
public class waveInfo 
{

    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
   
}