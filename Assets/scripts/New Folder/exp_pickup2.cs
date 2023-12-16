using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exp_pickup2 : MonoBehaviour
{
    public int expValue;
    private bool movingToPlayer;
    public float moveSpeed;
    public float timeBetweenChecks = 0.2f;//拾取的检查频率
    public float checkCounter;
    private player_controller2 player;
    // Start is called before the first frame update

    void Start()
    {
        player=player_health_controller2.instance.GetComponent<player_controller2>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movingToPlayer==true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            checkCounter -= Time.deltaTime;
            if(checkCounter <=0)
            {
                checkCounter = timeBetweenChecks;
                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange) 
                {
                    movingToPlayer = true;
                    moveSpeed += player.moveSpeed;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player2")
        {
            experience_level_controller2.instance.GetExp(expValue);
            Destroy(gameObject);
        }
    }
}
