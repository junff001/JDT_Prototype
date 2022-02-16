using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Action : MonoBehaviour
{
    private Player player; 

    protected Rigidbody2D rigid { get => player?.rigid; }

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
