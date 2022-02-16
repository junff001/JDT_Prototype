using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public override Rigidbody2D rigid { get; set; }
    public override BoxCollider2D collider { get; set; }

    protected override void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    protected override void Start()
    {
       
    }

    protected override void Update()
    {
        
    }
}
