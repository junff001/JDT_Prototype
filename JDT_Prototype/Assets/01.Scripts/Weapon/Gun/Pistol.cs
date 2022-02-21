using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    void OnEnable()
    {
        EventManager.AddEvent_Action("SHOTGUN_SHOOT", Shoot);
        EventManager.AddEvent_Action("SHOTGUN_RELOAD", Reload);
        EventManager.AddEvent_Function("SHOTGUN_BULLETCOUNT", BulletCount);
        EventManager.AddEvent_Function("SHOTGUN_RELOADTIME", ReloadTime);
    }

    protected override void Shoot()
    {
        if (gunData.bulletCount > 0)
        {
            //CameraAction.ShakeCam(10, 0.1f); // 카메라 흔들림 == 반동효과
            //GameManager.PlaySFX(GameManager.Instance.audioBox.p_shot_gun, 0.6f); // 사운드

            Bullet bullet = PoolManager.GetItem<Bullet>();
            bullet.transform.position = transform.GetChild(0).position;

            gunData.bulletCount--;
            EventManager.TriggerEvent_Action("CONSUMPTION_BULLET"); // 총알 소비 UI 
            StartCoroutine(ShootDelay());
        }
    }

    protected override void Reload()
    {
        if (gunData.bulletCount < gunData.maxBulletCount)
        {
            GameManager.PlaySFX(GameManager.Instance.audioBox.p_shot_gun_reload); // 사운드

            EventManager.TriggerEvent_Tween("FILLAMOUNTRELOAD_UI").onComplete += () =>
            {
                EventManager.TriggerEvent_Action("ONRELOADIMAGE_UI", false); // 재장전 바 On/Off
                EventManager.TriggerEvent_Action("FILLRELOADUIRESET"); // 재장전 바 리셋 
                EventManager.TriggerEvent_Action("LOAD_BULLET"); // 재장전 UI

                AfterImageManager.isOnAfterEffect = false;
                gunData.bulletCount = gunData.maxBulletCount;
            };
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
