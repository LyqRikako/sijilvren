using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller2 : MonoBehaviour
{
    public static player_controller2 instance;
    private void Awake()
    {
        instance = this; 
    }
    public float moveSpeed;//移动速度
    public Animator anim;
    public float pickupRange = 1.5f;//拾取距离，为拾取经验做准备
    //public weapon activeWeapon;
    public List<weapon> unassignedWeapons, assignedWeapons;
    public int maxWeapons = 3;

    [HideInInspector]
    public List<weapon> fullyLevelledweapons = new List<weapon>();
    internal float currentHealth;

    public float maxHealth { get; internal set; }

    // Start is called before the first frame update
    //初始时被调用的函数
    void Start()
    {
        if (assignedWeapons.Count == 0)
        {
            AddWeapon(Random.Range(0, unassignedWeapons.Count));
        }
        moveSpeed = playstatcontroller2.instance.moveSpeed[0].value;
        pickupRange = playstatcontroller2.instance.pickupRange[0].value;
        maxWeapons = Mathf.RoundToInt(playstatcontroller2.instance.maxWeapons[0].value);
    }
       

    // Update is called once per frame
    void Update()
    {
        
        Vector3 moveInput = new Vector3(0f, 0f, 0f);//设置一个三维向量，初始值为原点
        moveInput.x = Input.GetAxisRaw("HorizontalP2");//获取原始键盘水平输入
        moveInput.y = Input.GetAxisRaw("VerticalP2");//获取垂直方向键盘输入
        //Debug.Log(moveInput);
        moveInput.Normalize();//将移动输入标准化
        transform.position += moveInput*moveSpeed*Time.deltaTime;//移动距离与速度系数，时间变化率有关

        if(moveInput != Vector3.zero) {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void AddWeapon(int weaponNumber)
    {
        if(weaponNumber<unassignedWeapons.Count)
        {
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);
            unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            unassignedWeapons.RemoveAt(weaponNumber);
        }
    }
    public void AddWeapon(weapon weaponToAdd )
    {  
        weaponToAdd.gameObject.SetActive(true);
        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
    }
    }
