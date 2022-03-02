using System.Collections;
using UnityEngine;
using System;

public abstract class Gun : MonoBehaviour
{
    public GunData gunData;

    void Awake()
    {
        gunData.maxBulletCount = gunData.bulletCount;
        if (DataManager.sub == DataManager.Sub.swiftAttack)
        {
            gunData.attackCount = 2;
        }
    }

    protected virtual void Fire()
    {
        Debug.Log("공격");
        for (int i = 0; i < gunData.attackCount; i++)
        {
            Attack();
        }
    }

    protected abstract void Attack();

    public virtual void Reload()
    {
        Debug.Log("재장전");
        if (gunData.bulletCount < gunData.maxBulletCount)
        {
            StopCoroutine(ShootDelay());
            gunData.canShoot = true;

            GameManager.PlaySFX(GameManager.Instance.audioBox.p_shot_gun_reload); // ����

            //EventManager2.TriggerEvent_Tween("FILLAMOUNTRELOAD_UI").onComplete += () =>
            //{
            //    EventManager2.TriggerEvent_Action("ONRELOADIMAGE_UI", false); // ������ �� On/Off
            //    EventManager2.TriggerEvent_Action("FILLRELOADUIRESET"); // ������ �� ���� 
            //    EventManager2.TriggerEvent_Action("LOAD_BULLET"); // ������ UI
            //};

            AfterImageManager.isOnAfterEffect = false;
            gunData.bulletCount = gunData.maxBulletCount;
        }
    }

    protected virtual IEnumerator ShootDelay()
    {
        gunData.canShoot = false;
        yield return new WaitForSeconds(gunData.shootDelay);
        gunData.canShoot = true;
    }

    protected virtual int BulletCount() => gunData.bulletCount;
    protected virtual float ReloadTime() => gunData.reloadTime;
    protected virtual bool CanShoot() => gunData.canShoot;

    public virtual void InitData()
    {
        Debug.Log("무기 관련 함수 초기화");
    }
}
