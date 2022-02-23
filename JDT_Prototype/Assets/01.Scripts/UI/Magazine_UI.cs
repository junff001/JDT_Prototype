using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Magazine_UI : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab_UI;
    [SerializeField] private Transform bulletGroup_UI;

    private int currnetMagazine;

    private List<GameObject> magazine = new List<GameObject>();

    void Start()
    {
        currnetMagazine = magazine.Count;
        InitBullet_UI(); 
        OnImageBullet_UI(true);

        EventManager.AddEvent_Action("CONSUMPTIONBULLET_UI", ConsumptionBullet_UI);
        EventManager.AddEvent_Action("RELOADBULLET_UI", ReloadBullet_UI);
        
    }

    void InitBullet_UI()
    {
        for (int i = 0; i < EventManager.TriggerEvent_Int("BULLETCOUNT"); i++)
        {
            magazine.Add(Instantiate(bulletPrefab_UI, bulletGroup_UI));
        }
    }

    void ReloadBullet_UI() // 총알 장전
    {
        OnImageBullet_UI(true);
    }

    void ConsumptionBullet_UI() // 총알 소비
    {
        if (EventManager.TriggerEvent_Int("BULLETCOUNT") > 0)
        {
            magazine[currnetMagazine].gameObject.SetActive(false);
            currnetMagazine--;
            if (currnetMagazine < 0)
            {
                currnetMagazine = magazine.Count;
            }
        }
    }

    void OnImageBullet_UI(bool isOn) // On/Off
    {
        for (int i = 0; i < magazine.Count; i++)
        {
            magazine[i].gameObject.SetActive(isOn);
        }
    }

    void OnDisable()
    {
        EventManager.RemoveEvent("CONSUMPTION_BULLET");
        EventManager.RemoveEvent("LOAD_BULLET");
    }

    void OnDestroy()
    {
        EventManager.RemoveEvent("CONSUMPTION_BULLET");
        EventManager.RemoveEvent("LOAD_BULLET");
    }
}

