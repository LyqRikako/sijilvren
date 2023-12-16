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
    public float moveSpeed;//�ƶ��ٶ�
    public Animator anim;
    public float pickupRange = 1.5f;//ʰȡ���룬Ϊʰȡ������׼��
    //public weapon activeWeapon;
    public List<weapon> unassignedWeapons, assignedWeapons;
    public int maxWeapons = 3;

    [HideInInspector]
    public List<weapon> fullyLevelledweapons = new List<weapon>();
    internal float currentHealth;

    public float maxHealth { get; internal set; }

    // Start is called before the first frame update
    //��ʼʱ�����õĺ���
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
        
        Vector3 moveInput = new Vector3(0f, 0f, 0f);//����һ����ά��������ʼֵΪԭ��
        moveInput.x = Input.GetAxisRaw("HorizontalP2");//��ȡԭʼ����ˮƽ����
        moveInput.y = Input.GetAxisRaw("VerticalP2");//��ȡ��ֱ�����������
        //Debug.Log(moveInput);
        moveInput.Normalize();//���ƶ������׼��
        transform.position += moveInput*moveSpeed*Time.deltaTime;//�ƶ��������ٶ�ϵ����ʱ��仯���й�

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
