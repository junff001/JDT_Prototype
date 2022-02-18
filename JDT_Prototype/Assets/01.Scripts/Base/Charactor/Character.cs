using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public abstract Rigidbody2D rigid { get; set; }
    public abstract BoxCollider2D collider { get; set; }
    public abstract Animator animator { get; set; }
    
    protected abstract void Awake();
    protected abstract void Start();
    protected abstract void Update();
}
