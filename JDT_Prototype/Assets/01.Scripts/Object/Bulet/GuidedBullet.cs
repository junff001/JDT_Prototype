using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedBullet : BulletBase
{
    GameObject target;

    public override void OnEnable()
    {
        target = GameObject.Find("player");
    }


    protected override void Move()
    {
        base.Move();
        transform.LookAt(target.transform);
    }
}
