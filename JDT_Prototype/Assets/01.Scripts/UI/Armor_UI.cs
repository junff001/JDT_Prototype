using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Armor_UI : MonoBehaviour
{
    [SerializeField] private GameObject armorPrefab_Head_UI;
    [SerializeField] private GameObject armorPrefab_Body_UI;
    [SerializeField] private GameObject armorPrefab_Shoes_UI;

    [SerializeField] private Transform armor_Head;
    [SerializeField] private Transform armor_Body;
    [SerializeField] private Transform armor_Shoes;

    private GameObject head;
    private GameObject body;
    private GameObject shoes;

    private event Action Init_Armor;

    void Start()
    {
        Init_Armor = () =>
        {
            head = Instantiate(armorPrefab_Head_UI, armor_Head);
            body = Instantiate(armorPrefab_Body_UI, armor_Body);
            shoes = Instantiate(armorPrefab_Shoes_UI, armor_Shoes);
        };

        //Init_Armor(); // 방어구 UI 초기화

        EventManager.AddEvent("ADD_ARMOR", Add_Armor);
        EventManager.AddEvent("DESTROY_ARMOR", Destroy_Armor);
    }

    void Add_Armor(GameObject armor) // 방어구 흭득
    {
        if (armor.CompareTag("ArmorHead") && head == null)
        {
            Debug.Log("aslkdkhfgasfd");
            head = Instantiate(armorPrefab_Head_UI, armor_Head);
        }
        if (armor.CompareTag("ArmorBody") && body == null)
        {
            body = Instantiate(armorPrefab_Body_UI, armor_Body);
        }
        if (armor.CompareTag("ArmorShoes") && shoes == null)
        {
            shoes = Instantiate(armorPrefab_Shoes_UI, armor_Shoes);
        }
    }

    void Destroy_Armor(GameObject armor) // 방어구 파괴
    {
        if (armor.CompareTag("ArmorHead") && head != null)
        {
            Destroy(head);
        }
        if (armor.CompareTag("ArmorBody") && body != null)
        {
            Destroy(body);
        }
        if (armor.CompareTag("ArmorShoes") && shoes != null)
        {
            Destroy(shoes);
        }
    }
}

