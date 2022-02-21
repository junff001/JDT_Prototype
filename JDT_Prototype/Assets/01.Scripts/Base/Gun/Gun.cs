using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public GunData gunData;

    protected abstract void Shoot();
    protected abstract void Reload();

    protected virtual IEnumerator ShootDelay()
    {
        gunData.canShoot = false;
        yield return new WaitForSeconds(gunData.shootDelay);
        gunData.canShoot = true;
    }

    protected virtual int BulletCount() => gunData.bulletCount;
    protected virtual float ReloadTime() => gunData.reloadTime;
    protected virtual bool CanShoot() => gunData.canShoot;

}
    