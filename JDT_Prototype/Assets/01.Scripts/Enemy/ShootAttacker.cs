using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttacker : Enemy
{
    [SerializeField] private EnemyBullet bulletPrefab;
    Quaternion rot;
    private float angle;
    [SerializeField] private Transform firePoint;

    protected override void Update()
    {
        base.Update();
        angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        rot = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public override void Attack()
    {
        EnemyBullet bullet = PoolManager.GetItem<EnemyBullet>();
        bullet.transform.position = firePoint.transform.position;

        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rot.eulerAngles.z - 90));
    }
}
