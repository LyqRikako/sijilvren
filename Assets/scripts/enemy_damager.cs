using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_damager : MonoBehaviour
{
    public float damageAmount;//…À∫¶¡ø
    public bool shouldKnockback;
    public float timeBetweenDamage;
    public bool damageOverTime;
    private float damageCounter;
    private List<enemy_controller> enemiesInRange= new List<enemy_controller>();
    public bool destroyOnImpact;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (damageOverTime == true) 
        {
            damageCounter-= Time.deltaTime;
            if(damageCounter <= 0) 
            {
                damageCounter = timeBetweenDamage;
                for(int i = 0; i < enemiesInRange.Count; i++) 
                { 
                    if(enemiesInRange[i] != null)
                    { enemiesInRange[i].TakeDamage(damageAmount, shouldKnockback); }
                    else
                    {
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageOverTime == false)
        {
            if (collision.gameObject.tag == "enemy")
            {
                collision.GetComponent<enemy_controller>().TakeDamage(damageAmount, shouldKnockback);
                if (destroyOnImpact == true) { Destroy(gameObject); }
            }
        }
        else 
        {
            if (collision.gameObject.tag == "enemy")
            {
                enemiesInRange.Add(collision.GetComponent<enemy_controller>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (damageOverTime == true) 
        {
            if (collision.gameObject.tag == "enemy")
            {
                enemiesInRange.Remove(collision.GetComponent<enemy_controller>());
            }
        }
    }
}
