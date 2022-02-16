using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void OnDamage(Vector2 hitPoint, Vector2 normal);
}
