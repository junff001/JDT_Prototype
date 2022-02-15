using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAction : MonoBehaviour 
{
    protected abstract Character character { get; set; }

    protected abstract void Start();
    protected abstract void Update();
    protected abstract void FixedUpdate();
}
