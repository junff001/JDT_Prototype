using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerRotate : PlayerAction // �������� �˾Ƽ� ��������
{
    [HideInInspector] public float gunAngle = 0f;

    Vector2 mouse;
    [SerializeField] private GameObject player_Obj;

    [SerializeField] private GameObject gunPivot;
    [SerializeField] private GameObject crossHair;

    SpriteRenderer sr;
    private Camera mainCam;

    bool isInitCompleted = false;

    protected override void Start()
    {
        base.Start();
        
    }

    protected override void Update()
    {
        if (isInitCompleted)
        {
            CrosshairMove();
            Rotate();
        }
    }

    public void Init()
    {
        sr = gunPivot.GetComponentInChildren<SpriteRenderer>();
        mainCam = Camera.main;
        crossHair.SetActive(true);
        isInitCompleted = true;
    }

    void Rotate()
    {
        Vector3 dir = crossHair.transform.position - player_Obj.transform.position;
        if (dir.x > 0)
        {
            sr.flipY = true;
            gunAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180;
        }
        else
        {
            sr.flipY = false;
            gunAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        }
        gunPivot.transform.rotation = Quaternion.AngleAxis(gunAngle, Vector3.forward);
    }

    void CrosshairMove()
    {
        mouse = mainCam.ScreenToWorldPoint(Input.mousePosition);
        crossHair.transform.position = mouse;
    }
}
