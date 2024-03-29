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
            float angle = transform.parent.rotation.eulerAngles.z;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            bullet.transform.position = rGun.position;
            gunData.bulletCount--;
            EventManager2.TriggerEvent_Action("CONSUMPTION_BULLET"); // �Ѿ� �Һ� UI 
            EventManager2.TriggerEvent_Action("CURRENTBULLETCOUNT_UI", gunData.bulletCount);
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

            float angle = transform.parent.rotation.eulerAngles.z;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            bullet.transform.position = lGun.position;

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
        EventManager2.RemoveEvent("RELOADTIME");
    }
    public override void InitData()
    {
        base.InitData();

        EventManager2.AddEvent_Action("SHOOT", Fire);
        EventManager2.AddEvent_Action("RELOAD", Reload);
        EventManager2.AddEvent_Function("BULLETCOUNT", BulletCount);
        EventManager2.AddEvent_Function("RELOADTIME", ReloadTime);
    }
}
