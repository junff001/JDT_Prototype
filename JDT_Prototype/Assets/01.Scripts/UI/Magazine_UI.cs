using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Magazine_UI : MonoBehaviour
{
    [SerializeField] private Transform bullet_Group;
    [SerializeField] private GameObject bulletPrefab_UI;

    public GameObject first_Bullet;
    public GameObject second_Bullet;

    void Start()
    {
        Init_Bullet();

        //EventManager.AddEvent("CONSUMPTION_BULLET", Consumption_Bullet);
        //EventManager.AddEvent("LOAD_BULLET", Load_Bullet);
    }

    void Init_Bullet()
    {
        first_Bullet = Instantiate(bulletPrefab_UI, bullet_Group);
        second_Bullet = Instantiate(bulletPrefab_UI, bullet_Group);
    }

   public void Load_Bullet() // 총알 장전
    {
        Debug.Log("?");
        first_Bullet.SetActive(true);
        second_Bullet.SetActive(true);
    }

    void Consumption_Bullet() // 총알 소비
    {
        if (first_Bullet.activeSelf)
        {
            first_Bullet.SetActive(false);
        } 
        else if (second_Bullet.activeSelf)
        {
            second_Bullet.SetActive(false);
        }
        else
        {
            Debug.Log("남은 탄 없음");
        }
    }
}
