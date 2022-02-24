using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : Gun
{
    protected override void Shoot()
    {
        Debug.Log("Bazooka");

        if (gunData.bulletCount > 0 && CanShoot())
        {
            //CameraAction.ShakeCam(10, 0.1f); // ī�޶� ��鸲 == �ݵ�ȿ��
            //GameManager.PlaySFX(GameManager.Instance.audioBox.p_shot_gun, 0.6f); // ����

            ShotgunBullet bullet = PoolManager.GetItem<ShotgunBullet>();
            bullet.transform.position = transform.GetChild(0).position;

            gunData.bulletCount--;
            EventManager.TriggerEvent_Action("CONSUMPTION_BULLET"); // �Ѿ� �Һ� UI 
            EventManager.TriggerEvent_Action("CURRENTBULLETCOUNT_UI", gunData.bulletCount);
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

    public override void InitData()
    {
        EventManager.AddEvent_Action("BAZOOKA_SHOOT", Shoot);
        EventManager.AddEvent_Action("BAZOOKA_RELOAD", Reload);
        EventManager.AddEvent_Function("BAZOOKA_BULLETCOUNT", BulletCount);
        EventManager.AddEvent_Function("BAZOOKA_RELOADTIME", ReloadTime);
    }
}
