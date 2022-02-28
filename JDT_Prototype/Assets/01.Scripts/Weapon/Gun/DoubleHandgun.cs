using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleHandgun : Gun
{
    public Transform rGun;
    public Transform lGun;

    protected override void Attack()
    {
        StartCoroutine(SwitftAttack());
    }

    IEnumerator SwitftAttack()
    {
        RAttack();
        yield return new WaitForSeconds(0.1f);
        LAttack();
    }

    private void RAttack()
    {
        if (gunData.bulletCount > 0 && CanShoot())
        {
            //CameraAction.ShakeCam(10, 0.1f); // ī�޶� ��鸲 == �ݵ�ȿ��
            //GameManager.PlaySFX(GameManager.Instance.audioBox.p_shot_gun, 0.6f); // ����

            BulletBase bullet = PoolManager.GetItem<BulletBase>();
            bullet.transform.position = rGun.position;

            gunData.bulletCount--;
            EventManager.TriggerEvent_Action("CONSUMPTION_BULLET"); // �Ѿ� �Һ� UI 
            EventManager.TriggerEvent_Action("CURRENTBULLETCOUNT_UI", gunData.bulletCount);
            StartCoroutine(ShootDelay());
        }
    }

    private void LAttack()
    {
        if (gunData.bulletCount > 0 && CanShoot())
        {
            //CameraAction.ShakeCam(10, 0.1f); // ī�޶� ��鸲 == �ݵ�ȿ��
            //GameManager.PlaySFX(GameManager.Instance.audioBox.p_shot_gun, 0.6f); // ����

            BulletBase bullet = PoolManager.GetItem<BulletBase>();
            bullet.transform.position = lGun.position;

            gunData.bulletCount--;
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
        EventManager.AddEvent_Action("DOUBLEHANDGUN_SHOOT", OnFire);
        EventManager.AddEvent_Action("DOUBLEHANDGUN_RELOAD", Reload);
        EventManager.AddEvent_Function("DOUBLEHANDGUN_BULLETCOUNT", BulletCount);
        EventManager.AddEvent_Function("DOUBLEHANDGUN_RELOADTIME", ReloadTime);
    }
}
