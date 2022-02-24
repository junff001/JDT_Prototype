using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleHandgun : Gun
{
    protected override void Shoot()
    {
        Debug.Log("DoubleHandgun");
        if (gunData.bulletCount > 0 && CanShoot())
        {
            //CameraAction.ShakeCam(10, 0.1f); // ī�޶� ��鸲 == �ݵ�ȿ��
            //GameManager.PlaySFX(GameManager.Instance.audioBox.p_shot_gun, 0.6f); // ����

            ShotgunBullet bullet = PoolManager.GetItem<ShotgunBullet>();
            bullet.transform.position = transform.GetChild(0).position;

            gunData.bulletCount -= 2;
            EventManager.TriggerEvent_Action("CONSUMPTION_BULLET"); // �Ѿ� �Һ� UI 
            EventManager.TriggerEvent_Action("CURRENTBULLETCOUNT_UI", gunData.bulletCount);
            StartCoroutine(ShootDelay());
        }
    }

    void OnDestroy()
    {
        EventManager.RemoveEvent("DOUBLEHANDGUN_SHOOT");
        EventManager.RemoveEvent("DOUBLEHANDGUN_RELOAD");
        EventManager.RemoveEvent("DOUBLEHANDGUN_BULLETCOUNT");
        EventManager.RemoveEvent("DOUBLEHANDGUN_RELOADTIME");
    }

    public override void InitData()
    {
        EventManager.AddEvent_Action("DOUBLEHANDGUN_SHOOT", Shoot);
        EventManager.AddEvent_Action("DOUBLEHANDGUN_RELOAD", Reload);
        EventManager.AddEvent_Function("DOUBLEHANDGUN_BULLETCOUNT", BulletCount);
        EventManager.AddEvent_Function("DOUBLEHANDGUN_RELOADTIME", ReloadTime);
    }
}
