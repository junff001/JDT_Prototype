using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : Gun
{
    protected override void Attack()
    {
        Debug.Log("실행은 됐고");
        if (gunData.bulletCount > 0 && CanShoot())
        {
            Debug.Log("진짜 공격");
            //CameraAction.ShakeCam(10, 0.1f); // ī�޶� ��鸲 == �ݵ�ȿ��
            //GameManager.PlaySFX(GameManager.Instance.audioBox.p_shot_gun, 0.6f); // ����

            BulletBase bullet = PoolManager.GetItem<BulletBase>();

            float angle = transform.parent.rotation.eulerAngles.z;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            bullet.transform.position = transform.GetChild(0).position;

            gunData.bulletCount--;
            EventManager2.TriggerEvent_Action("CONSUMPTION_BULLET"); // �Ѿ� �Һ� UI 
            EventManager2.TriggerEvent_Action("CURRENTBULLETCOUNT_UI", gunData.bulletCount);
            StartCoroutine(ShootDelay());
        }
    }

    void OnDestroy()
    {
        EventManager2.RemoveEvent("SHOOT");
        EventManager2.RemoveEvent("RELOAD");
        EventManager2.RemoveEvent("BULLETCOUNT");
        EventManager2.RemoveEvent("BAZOOKA_RELOADTIME");
    }

    public override void InitData()
    {
        EventManager2.AddEvent_Action("SHOOT", Fire);
        EventManager2.AddEvent_Action("RELOAD", Reload);
        EventManager2.AddEvent_Function("BULLETCOUNT", BulletCount);
        EventManager2.AddEvent_Function("RELOADTIME", ReloadTime);
    }
}
