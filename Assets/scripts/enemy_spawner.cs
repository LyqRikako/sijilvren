using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy_spawner : MonoBehaviour
{
    public GameObject enemyToSpawn;//��һ�����ɹ����ģ��,���ģ����õ���д�õ�slimeģ��
    public float timeToSpawn;
    private float spawmCounter;//���ɹ���ļ�ʱ��
    public Transform minSpawn, maxSpawn;
    private Transform target;
    private float despawnDistance;//����������Զ���룬����������룬С�ֻ����
    private List<GameObject> spawnEnemies = new List<GameObject>();
    public int checkPerFrame;//ÿ֡Ҫ���ĵ�������
    private int enemyToCheck;//�鿴�ı�ǵ�

    public List<waveInfo> waves;//һ��һ�������ɵ���
    private int currentWave;//��ǰ����
    private float waveCounter;//ʱ�䵹��ʱ
    // Start is called before the first frame update
    void Start()
    {
        //spawmCounter = timeToSpawn;
        target = player_health_controller.instance.transform;
        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 6f;//��ˢ�ֵ��6������
        currentWave = -1;
    }

    // Update is called once per frame
    void Update()
    {
        //������ԭ���������ɹ���ķ�����֮�����һ��һ��ˢ��
        /*spawmCounter -= Time.deltaTime;
        if (spawmCounter<=0)
        {
            spawmCounter = timeToSpawn;
            GameObject newEnemy= Instantiate(enemyToSpawn, selectSpawnPoint(), transform.rotation);
            spawnEnemies.Add(newEnemy);//ÿ������һ������󣬽����������ǵ��б�
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
                    spawnEnemies.Add(newEnemy);//ÿ������һ������󣬽����������ǵ��б�
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

[System.Serializable]//���л�����unity�п�����ʾ
public class waveInfo 
{

    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
   
}