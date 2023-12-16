using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_animation : MonoBehaviour
{
    public Transform slime_blue;
    public float speed;
    public float minSize;//最小比例
    public float maxSize;//最大比例
    private float activeSize;//当前活动比例
    // Start is called before the first frame update
    void Start()
    {
        activeSize = maxSize;
    }

    // Update is called once per frame
    void Update()
    {
        slime_blue.localScale = Vector3.MoveTowards(slime_blue.localScale, Vector3.one * activeSize, speed * Time.deltaTime);
        if (slime_blue.localScale.x == activeSize)
        {
            if (activeSize == maxSize)
            {
                activeSize = minSize;
            }
            else 
            {
                activeSize = maxSize;
            }
        }
    }
}
