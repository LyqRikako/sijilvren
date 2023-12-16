using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exp_pickup : MonoBehaviour
{
    public int expValue;
    private bool movingToPlayer;
    public float moveSpeed;
    public float timeBetweenChecks = 0.2f;//拾取的检查频率
    public float checkCounter;
    private player_controller player;
    private player_controller2 player2;
    private int flag;
    // Start is called before the first frame update

    void Start()
    {
        player=player_health_controller.instance.GetComponent<player_controller>();
        player2 = player_health_controller2.instance.GetComponent<player_controller2>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movingToPlayer==true)
        {
            if (flag == 1) { transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime); }
            if (flag == 2) { transform.position = Vector3.MoveTowards(transform.position, player2.transform.position, moveSpeed * Time.deltaTime); }
        }
        else
        {
            checkCounter -= Time.deltaTime;
            if(checkCounter <=0)
            {
                checkCounter = timeBetweenChecks;
                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange&& (Vector3.Distance(transform.position, player.transform.position)< Vector3.Distance(transform.position, player2.transform.position))) 
                {
                    movingToPlayer = true;
                    flag = 1;
                    moveSpeed += player.moveSpeed;
                }
                if (Vector3.Distance(transform.position, player2.transform.position) < player2.pickupRange&& (Vector3.Distance(transform.position, player2.transform.position)< Vector3.Distance(transform.position, player.transform.position)))
                {
                    movingToPlayer = true;
                    flag = 2;
                    moveSpeed += player2.moveSpeed;
                }

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            experience_level_controller.instance.GetExp(expValue);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player2")
        {
            experience_level_controller2.instance.GetExp(expValue);
            Destroy(gameObject);
        }
    }
}
