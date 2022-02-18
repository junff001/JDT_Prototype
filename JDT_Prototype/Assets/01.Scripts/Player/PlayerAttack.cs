using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;
using DG.Tweening;

public class PlayerAttack : PlayerAction
{
    [SerializeField] private GameObject bullet;

    protected override void Start()
    {
        base.Start();
        PoolManager.CreatePool<Bullet>(bullet, transform, 100);
        EventManager.AddEvent_Action("ATTACK", Attack);
        EventManager.AddEvent_Action("RELOAD", Reload);
    }

    protected override void Update()
    {

    }

    void Reload()
    {
        EventManager.TriggerEvent_Action("SHOTGUN_RELOAD");
    }

    void Attack()
    {
        EventManager.TriggerEvent_Action("SHOTGUN_SHOOT");
    }

    void OnDestroy()
    {
        EventManager.RemoveEvent("ATTACK");
        EventManager.RemoveEvent("RELOAD");
    }
}
