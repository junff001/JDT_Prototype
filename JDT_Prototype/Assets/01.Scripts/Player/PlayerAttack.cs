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

        switch (DataManager.sub)
        {
            case DataManager.Sub.none:
                bullet = Resources.Load("NormalBullet") as GameObject;
                break;

            case DataManager.Sub.bulletDivision:
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

        PoolManager.CreatePool<BulletBase>(bullet, transform, 100);
    }
}
