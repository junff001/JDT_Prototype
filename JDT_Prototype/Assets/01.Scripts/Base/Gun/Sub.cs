using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sub : MonoBehaviour
{
    public float damage;
    public float speed;

    public abstract void Attack();
}
