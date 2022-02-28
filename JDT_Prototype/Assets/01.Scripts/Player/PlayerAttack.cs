using UnityEngine;
using System;

public class PlayerAttack : PlayerAction
{
    private GameObject bullet;

    public Action Reload;
    public Action Attack;

    protected override void Start()
    {
        base.Start();
    }

    void OnDestroy()
    {
        EventManager.RemoveEvent("ATTACK");
        EventManager.RemoveEvent("RELOAD");
    }

    public void InitData()
    {
        Reload = () => EventManager.TriggerEvent_Action("SHOTGUN_RELOAD");
        Attack = () => EventManager.TriggerEvent_Action("SHOTGUN_SHOOT");

        EventManager.AddEvent_Action("ATTACK", Attack);
        EventManager.AddEvent_Action("RELOAD", Reload);

        bullet = Resources.Load()

        PoolManager.CreatePool<BulletBase>(bullet, transform, 100);

        /*
        switch (DataManager.sub)
        {
            case DataManager.Sub.none:
                PoolManager.CreatePool<NormalBullet>(bullet, transform, 100);
                break;

            case DataManager.Sub.bulletDivision:
                PoolManager.CreatePool<DivisionBullet>(bullet, transform, 100);
                break;

            case DataManager.Sub.exploisionBullet:
                PoolManager.CreatePool<ExplosionBullet>(bullet, transform, 100);
                break;

            case DataManager.Sub.guidedMissile:
                PoolManager.CreatePool<GuidedBullet>(bullet, transform, 100);
                break;

            case DataManager.Sub.swiftAttack:
                PoolManager.CreatePool<SwiftBullet>(bullet, transform, 100);
                break;
        }
        */
    }
}
