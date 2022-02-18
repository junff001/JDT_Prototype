using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private Player player; 

    protected Rigidbody2D rigid { get => player?.rigid; }
    protected BoxCollider2D collider { get => player?.collider; }
    protected Animator animator { get => player?.animator; }

    public event Action<Quaternion> SetRotation;

    protected virtual void Start()
    {
        player = GetComponent<Player>();

        SetRotation += (x) => this.transform.rotation = x;
    }

    protected virtual void Update()
    {

    }
}
