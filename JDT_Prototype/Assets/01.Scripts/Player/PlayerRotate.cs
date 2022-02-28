using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerRotate : PlayerAction // �������� �˾Ƽ� ��������
{
    [HideInInspector] public float playerAngle = 0f;
    [HideInInspector] public float gunAngle = 0f;

    Vector2 mouse;
    [SerializeField] private GameObject player_Obj;
    [SerializeField] private GameObject gunPivot;
    [SerializeField] private GameObject crossHair;

    SpriteRenderer sr;

    private Camera mainCam;

    protected override void Start()
    {
        base.Start();
        sr = gunPivot.GetComponentInChildren<SpriteRenderer>();
        mainCam = Camera.main;
        crossHair.SetActive(true);
    }

    protected override void Update()
    {
        CrosshairMove();
        Rotate();
    }

    void Rotate()
    {
        Vector3 dir = crossHair.transform.position - player_Obj.transform.position;
        if (dir.x > 0)
        {
            sr.flipY = true;
            playerAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 270;
        }
        else
        {
            sr.flipY = false;
            playerAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180;
        }
        gunPivot.transform.rotation = Quaternion.AngleAxis(playerAngle, Vector3.forward);
    }

    void CrosshairMove()
    {
        mouse = mainCam.ScreenToWorldPoint(Input.mousePosition);
        crossHair.transform.position = mouse;
    }
}
