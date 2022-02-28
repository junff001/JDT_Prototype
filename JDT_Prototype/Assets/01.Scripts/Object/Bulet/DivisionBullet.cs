using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivisionBullet : BulletBase
{
    public bool canDivide;

    public override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(DivideShoot());
    }

    IEnumerator DivideShoot()
    {
        if (canDivide)
        {
            yield return new WaitForSeconds(0.5f);

            BulletBase bullet1 = PoolManager.GetItem<BulletBase>();
            BulletBase bullet2 = PoolManager.GetItem<BulletBase>();

            bullet1.transform.rotation = Quaternion.AngleAxis(transform.eulerAngles.z + 15, Vector3.forward);
            bullet2.transform.rotation = Quaternion.AngleAxis(transform.eulerAngles.z - 15, Vector3.forward);

            bullet1.GetComponent<DivisionBullet>().canDivide = false;
            bullet2.GetComponent<DivisionBullet>().canDivide = false;

            currentSpeed = 0;
            gameObject.SetActive(false);
        }
        yield return null;
        
    }

    public void OnDisable()
    {
        canDivide = true;
    }
}
