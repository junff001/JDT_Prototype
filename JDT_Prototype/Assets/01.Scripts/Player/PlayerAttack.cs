using UnityEngine;
using System;

public class PlayerAttack : PlayerAction
{
    [SerializeField] private GameObject bullet;

    public Action Reload;
    public Action Attack;

    protected override void Start()
    {
        base.Start();
        PoolManager.CreatePool<Bullet>(bullet, transform, 100);
        
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
    }
}
