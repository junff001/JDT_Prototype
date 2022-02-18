using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shotgun : Gun
{
    [SerializeField] private float shootDelay = 0.5f;
    [SerializeField] private float shootAngle = 30;
    [SerializeField] private int bulletCount = 2;  
    [SerializeField] private float reloadTime = 0.7f;
    [SerializeField] private int buckshot_BulletCount = 5;

    void Start()
    {
        EventManager.AddEvent_Action("SHOTGUN_SHOOT", Shoot);
        EventManager.AddEvent_Action("SHOTGUN_RELOAD", Reload);
        EventManager.AddEvent_Function("SHOTGUN_BULLETCOUNT", BulletCount);
        EventManager.AddEvent_Function("SHOTGUN_RELOADTIME", ReloadTime);
    }

    protected override void Shoot()
    {
        if (bulletCount > 0)
        {
            CameraAction.ShakeCam(10, 0.1f); // 카메라 흔들림 == 반동효과
            GameManager.PlaySFX(GameManager.Instance.audioBox.p_shot_gun, 0.6f); // 사운드
            TextureParticleManager.Instance.SpawnShell(transform.position, EventManager.TriggerEvent_Vector3("GETEJECTDIRECTION")); // 셰이더 그래프

            float beforeAngle = transform.rotation.eulerAngles.z - (shootAngle / 2);

            for (int i = 0; i < buckshot_BulletCount; i++)
            {
                Bullet bullet = PoolManager.GetItem<Bullet>();
                bullet.transform.position = transform.GetChild(0).position;
                bullet.transform.rotation =
                    Quaternion.Euler(new Vector3(0, 0, (beforeAngle + (shootAngle / (buckshot_BulletCount - 1)) * i) - 90));
            }

            bulletCount--;
            EventManager.TriggerEvent_Action("CONSUMPTION_BULLET"); // 총알 소비 UI 
            StartCoroutine(ShootDelay());
        }
    }

    protected override void Reload()
    {
        if (bulletCount < 2)
        {
            GameManager.PlaySFX(GameManager.Instance.audioBox.p_shot_gun_reload); // 사운드

            EventManager.TriggerEvent_Tween("FILLAMOUNTRELOAD_UI").onComplete += () =>
            {
                EventManager.TriggerEvent_Action("ONRELOADIMAGE_UI", false); // 재장전 바 On/Off
                EventManager.TriggerEvent_Action("FILLRELOADUIRESET"); // 재장전 바 리셋 
                EventManager.TriggerEvent_Action("LOAD_BULLET"); // 재장전 UI

                AfterImageManager.isOnAfterEffect = false;
                bulletCount = 2;
            };
        }
    }

    protected override IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
    }

    protected override int BulletCount()
    {
        return bulletCount;
    }

    protected override float ReloadTime()
    {
        return reloadTime;
    }

    void OnDestroy()
    {
        EventManager.RemoveEvent("SHOTGUN_SHOOT");
        EventManager.RemoveEvent("SHOTGUN_RELOAD");
        EventManager.RemoveEvent("SHOTGUN_BULLETCOUNT");
        EventManager.RemoveEvent("SHOTGUN_RELOADTIME");
    }
}
