using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public GunData gunData;

    public Sub sub;

    private void Awake()
    {
        gunData.maxBulletCount = gunData.bulletCount;
    }

    protected abstract void Shoot();

    protected virtual void Reload()
    {
        if (gunData.bulletCount < gunData.maxBulletCount)
        {
            StopCoroutine(ShootDelay());
            gunData.canShoot = true;

            GameManager.PlaySFX(GameManager.Instance.audioBox.p_shot_gun_reload); // ����

            EventManager.TriggerEvent_Tween("FILLAMOUNTRELOAD_UI").onComplete += () =>
            {
                EventManager.TriggerEvent_Action("ONRELOADIMAGE_UI", false); // ������ �� On/Off
                EventManager.TriggerEvent_Action("FILLRELOADUIRESET"); // ������ �� ���� 
                EventManager.TriggerEvent_Action("LOAD_BULLET"); // ������ UI

                AfterImageManager.isOnAfterEffect = false;
                gunData.bulletCount = gunData.maxBulletCount;
            };
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
        switch (DataManager.weapon)
        {

        }
    }
}
