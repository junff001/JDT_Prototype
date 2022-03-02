using UnityEngine;
using System;

public class PlayerAttack : PlayerAction
{
    private GameObject bullet;


    protected override void Start()
    {
        base.Start();
    }

    public void InitData()
    { 

        switch (DataManager.sub)
        {
            case DataManager.Sub.none:
                bullet = Resources.Load("NormalBullet") as GameObject;
                break;

            case DataManager.Sub.bulletDivision:
                bullet = Resources.Load("NormalBullet") as GameObject;
                PoolManager.CreatePool<NormalBullet>(bullet, GameManager.Instance.transform, 50);

                bullet = Resources.Load("DivisionBullet") as GameObject;
                break;

            case DataManager.Sub.exploisionBullet:
                bullet = Resources.Load("ExplosionBullet") as GameObject;
                break;

            case DataManager.Sub.guidedMissile:
                bullet = Resources.Load("GuidedBullet") as GameObject;
                break;

            case DataManager.Sub.swiftAttack:
                bullet = Resources.Load("SwiftBullet") as GameObject;
                break;
        }

        PoolManager.CreatePool<BulletBase>(bullet, GameManager.Instance.transform, 100);
    }
}
