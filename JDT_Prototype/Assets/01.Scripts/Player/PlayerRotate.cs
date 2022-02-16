using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerRotate : PlayerAction // �������� �˾Ƽ� ��������
{
    [HideInInspector]public float playerAngle = 0f;
    [HideInInspector]public float gunAngle = 0f;

    Vector2 mouse;
    [SerializeField] private GameObject player_Obj;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject crossHair;

    private Camera mainCam;

    protected override void Start()
    {
        base.Start();
        mainCam = Camera.main;
        crossHair.SetActive(true);
    }
    

    protected override void Update()
    {
        //if (PlayerMove.isDead) return;

        CrosshairMove();
        Rotate();
    }


    void Rotate()
    {
        Vector3 dir = crossHair.transform.position - player_Obj.transform.position;

        playerAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180f;
        // �̰� ������� ��������Ʈ �ٲٸ� ���� �������?

        gunAngle = Mathf.Atan2(mouse.y - gun.transform.position.y, mouse.x - gun.transform.position.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.AngleAxis(gunAngle, Vector3.forward);
    }
    
    void CrosshairMove()
    {
        mouse = mainCam.ScreenToWorldPoint(Input.mousePosition);
        crossHair.transform.position = mouse;
    }
}
