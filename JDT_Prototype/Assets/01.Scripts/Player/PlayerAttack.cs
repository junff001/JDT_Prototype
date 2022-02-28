using UnityEngine;
using System;

public class PlayerAttack : PlayerAction
{
    private GameObject bullet;

    event Action OnReload;
    event Action OnAttack;

    protected override void Start()
    {
        base.Start();
    }

    void OnDestroy()
    {
        EventManager.RemoveEvent("ATTACK");
        EventManager.RemoveEvent("RELOAD");

        //EventManager2.RemoveEvent("ATTACK");
        //EventManager2.RemoveEvent("RELOAD");
    }

    public void InitData()
    { 
        OnReload = () => EventManager.TriggerEvent("RELOAD");
        OnAttack = () => EventManager.TriggerEvent("SHOOT");

        //Reload = () => EventManager2.TriggerEvent_Action("RELOAD");
        //Attack = () => EventManager2.TriggerEvent_Action("SHOOT");

        EventManager.AddEvent("ATTACK", OnAttack);
        EventManager.AddEvent("RELOAD", OnReload);

        //EventManager2.AddEvent_Action("ATTACK", Attack);
        //EventManager2.AddEvent_Action("RELOAD", Reload);

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
