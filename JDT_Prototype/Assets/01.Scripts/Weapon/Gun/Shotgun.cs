using System;
using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] private int buckshot_BulletCount = 5;
    [SerializeField] private float shootAngle = 30;

    void Start()
    {
        EventManager.AddEvent_Action("SHOTGUN_SHOOT", Shoot);
        EventManager.AddEvent_Action("SHOTGUN_RELOAD", Reload);
        EventManager.AddEvent_Function("SHOTGUN_BULLETCOUNT", BulletCount);
        EventManager.AddEvent_Function("SHOTGUN_RELOADTIME", ReloadTime);
    }

    protected override void Shoot()
    {
        if (gunData.bulletCount > 0 && CanShoot())
        {
            //CameraAction.ShakeCam(10, 0.1f); // 카메라 흔들림 == 반동효과
            //GameManager.PlaySFX(GameManager.Instance.audioBox.p_shot_gun, 0.6f); // 사운드

            float beforeAngle = transform.rotation.eulerAngles.z - (shootAngle / 2);

            for (int i = 0; i < buckshot_BulletCount; i++)
            {
                Bullet bullet = PoolManager.GetItem<Bullet>();
                bullet.transform.position = transform.GetChild(0).position;
                bullet.transform.rotation =
                    Quaternion.Euler(new Vector3(0, 0, (beforeAngle + (shootAngle / (buckshot_BulletCount - 1)) * i) - 90));
            }

            gunData.bulletCount--;
            EventManager.TriggerEvent_Action("CONSUMPTION_BULLET"); // 총알 소비 UI 
            StartCoroutine(ShootDelay());
        }
    }



    void OnDestroy()
    {
        EventManager.RemoveEvent("SHOTGUN_SHOOT");
        EventManager.RemoveEvent("SHOTGUN_RELOAD");
        EventManager.RemoveEvent("SHOTGUN_BULLETCOUNT");
        EventManager.RemoveEvent("SHOTGUN_RELOADTIME");
    }
}
