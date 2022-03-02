using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivisionBullet : BulletBase
{

    public override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(DivideShoot());
    }

    IEnumerator DivideShoot()
    {
        yield return new WaitForSeconds(0.5f);

        NormalBullet bullet1 = PoolManager.GetItem<NormalBullet>();
        NormalBullet bullet2 = PoolManager.GetItem<NormalBullet>();

        bullet1.transform.position = this.transform.position;
        bullet2.transform.position = this.transform.position;

        bullet1.transform.rotation = Quaternion.AngleAxis(transform.eulerAngles.z + 15, Vector3.forward);
        bullet2.transform.rotation = Quaternion.AngleAxis(transform.eulerAngles.z - 15, Vector3.forward);

        currentSpeed = 0;
        gameObject.SetActive(false);
    }
}
