using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : Enemy
{
    [SerializeField] private float explosionRadius;
    [SerializeField] ParticleSystem explosionParticle;

    public override void Attack()
    {
        explosionParticle?.Play();
        if (Vector3.Distance(target.transform.position, transform.position) < explosionRadius)
        {
            target.GetComponent<IDamageable>()?.OnDamage(target.transform.position, this.transform.position);
        }
        Destroy(this.gameObject);
    }
}
