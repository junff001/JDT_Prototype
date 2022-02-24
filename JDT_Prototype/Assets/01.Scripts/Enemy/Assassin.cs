using UnityEngine;

public class Assassin : Enemy
{
    [SerializeField] private float explosionRadius;
    [SerializeField] ParticleSystem explosionParticle;

    public override void Attack()
    {
        Instantiate(explosionParticle,transform.position,Quaternion.identity);
        
        if (Vector3.Distance(target.transform.position, transform.position) < explosionRadius)
        {
            target.GetComponent<IDamageable>()?.OnDamage(5);
        }
        Destroy(this.gameObject);
    }
}
