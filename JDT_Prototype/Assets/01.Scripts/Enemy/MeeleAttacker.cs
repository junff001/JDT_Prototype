using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleAttacker : Enemy
{
    public override void Attack()
    {
        Debug.Log("근접 공격(몸통박치기)");
    }
}

