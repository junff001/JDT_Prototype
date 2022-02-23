using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : Gun
{
    void Start()
    {
        EventManager.AddEvent_Action("BAZOOKA_SHOOT", Shoot);
        EventManager.AddEvent_Action("BAZOOKA_RELOAD", Reload);
        EventManager.AddEvent_Function("BAZOOKA_BULLETCOUNT", BulletCount);
        EventManager.AddEvent_Function("BAZOOKA_RELOADTIME", ReloadTime);
    }

    protected override void Shoot()
    {
        if (gunData.bulletCount > 0 && CanShoot())
        {
            //CameraAction.ShakeCam(10, 0.1f); // ī�޶� ��鸲 == �ݵ�ȿ��
            //GameManager.PlaySFX(GameManager.Instance.audioBox.p_shot_gun, 0.6f); // ����

            Bullet bullet = PoolManager.GetItem<Bullet>();
            bullet.transform.position = transform.GetChild(0).position;

            gunData.bulletCount--;
            EventManager.TriggerEvent_Action("CONSUMPTION_BULLET"); // �Ѿ� �Һ� UI 
            StartCoroutine(ShootDelay());
        }
    }

    void OnDestroy()
    {
        EventManager.RemoveEvent("BAZOOKA_SHOOT");
        EventManager.RemoveEvent("BAZOOKA_RELOAD");
        EventManager.RemoveEvent("BAZOOKA_BULLETCOUNT");
        EventManager.RemoveEvent("BAZOOKA_RELOADTIME");
    }
}
