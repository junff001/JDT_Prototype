using System;
using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] private int buckshot_BulletCount = 5;
    [SerializeField] private float shootAngle = 30;

    protected override void Attack()
    {
        if (gunData.bulletCount > 0 && CanShoot())
        {
            //CameraAction.ShakeCam(10, 0.1f); // 카메라 흔들림 == 반동효과
            //GameManager.PlaySFX(GameManager.Instance.audioBox.p_shot_gun, 0.6f); // 사운드

            float beforeAngle = transform.parent.rotation.eulerAngles.z - (shootAngle / 2);

            for (int i = 0; i < buckshot_BulletCount; i++)
            {
                BulletBase bullet = PoolManager.GetItem<BulletBase>();
                bullet.transform.position = transform.GetChild(0).position;
                bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (beforeAngle + (shootAngle / (buckshot_BulletCount - 1)) * i) - 90));

            }

            gunData.bulletCount--;
            EventManager.TriggerEvent_Action("CONSUMPTION_BULLET"); // 총알 소비 UI 
            EventManager.TriggerEvent_Action("CURRENTBULLETCOUNT_UI", gunData.bulletCount);
            StartCoroutine(ShootDelay());
        }
    }

    void OnDestroy()
    {
        EventManager.RemoveEvent("SHOOT");
        EventManager.RemoveEvent("RELOAD");
        EventManager.RemoveEvent("BULLETCOUNT");
        EventManager.RemoveEvent("RELOADTIME");
    }

    public override void InitData()
    {
        EventManager.AddEvent_Action("SHOOT", OnFire);
        EventManager.AddEvent_Action("RELOAD", Reload);
        EventManager.AddEvent_Function("BULLETCOUNT", BulletCount);
        EventManager.AddEvent_Function("RELOADTIME", ReloadTime);
    }

}
