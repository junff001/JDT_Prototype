using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    protected abstract void Shoot();
    protected abstract void Reload();
    protected abstract IEnumerator ShootDelay();
    protected abstract int BulletCount();
    protected abstract float ReloadTime();
}
    