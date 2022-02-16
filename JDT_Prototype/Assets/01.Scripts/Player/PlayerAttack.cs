using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class PlayerAttack : Player_Action
{
    public float shootAngle = 30;
    public int buckshot_BulletCount = 5;
    public int bulletCount = 2;
    private bool isDelay;

    private bool isReloading = false;

    public float reloadTime { get; private set; } = 2;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform gun;
    [SerializeField] private float attack_Delay = 0.5f;



    protected override void Start()
    {
        base.Start();
        GameManager.Instance.OnReload += Reload;
        PoolManager.CreatePool<Bullet>(bullet, transform, 100);

    }

    protected override void Update()
    {
        if (PlayerMove.isDead) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (CanShoot())
            {
                if (!isDelay)
                {
                    isDelay = true;
                    Shoot(); //����
                    StartCoroutine(Attack_Delay());
                }
            }
        }

        if (Input.GetButtonDown("Reload")) // R 키
        {
            Reload();
        }
    }

    void Shoot()
    {
        CameraAction.ShakeCam(10, 0.1f);
        GameManager.PlaySFX(GameManager.Instance.audioBox.p_shot_gun, 0.6f);
        //TextureParticleManager.Instance.SpawnShell(transform.position, GetEjectDirection());

        float beforeAngle = gun.transform.rotation.eulerAngles.z - (shootAngle / 2);

        for (int i = 0; i < buckshot_BulletCount; i++)
        {
            Bullet bullet = PoolManager.GetItem<Bullet>();
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (beforeAngle + (shootAngle / (buckshot_BulletCount - 1)) * i) - 90));
        }

        bulletCount--;
        EventManager.TriggerEvent("CONSUMPTION_BULLET"); // 총알 소비 UI 
    }

    public Vector3 GetEjectDirection()
    {
        if (transform.localScale.y < 0)
        {
            return (transform.right * -0.5f + transform.up).normalized;
        }
        else
        {
            return (transform.right * 0.5f + transform.up).normalized * -1;
        }
    }

    void Reload()
    {
        if (bulletCount < 2 && !isReloading)
        {
            isReloading = true;
            GameManager.PlaySFX(GameManager.Instance.audioBox.p_shot_gun_reload);
            EventManager.TriggerEvent("FILL_AMOUNT");
            Invoke("ReloadEnd", reloadTime);
        }
    }

    public void ReloadEnd()
    {
        bulletCount = 2;
        isReloading = false;
    }

    public void DashReload()
    {
        EventManager.TriggerEvent("LOAD_BULLET");
        ReloadEnd();
    }


    bool CanShoot() // IsShoot
    {
        if(bulletCount > 0 && !isReloading)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator Attack_Delay()
    {
        yield return new WaitForSeconds(attack_Delay);
        isDelay = false;
    }
}
