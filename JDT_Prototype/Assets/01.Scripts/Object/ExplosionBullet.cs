using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private float currentSpeed;
    public float damage;
    [SerializeField] private GameObject bullet;
    private Collider2D _collider2D;

    public ParticleSystem explosionEfx;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        currentSpeed = speed;
        bullet.SetActive(true);
        _collider2D.enabled = true;

        StartCoroutine(BulletDestroy(0.2f));
    }

    private void Update()
    {
        transform.Translate(transform.up * currentSpeed * Time.deltaTime, Space.World);
    }

    IEnumerator BulletDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        currentSpeed = 0;
        bullet.SetActive(false);
        _collider2D.enabled = false;
        
    }

    public void Attack()
    {
        Instantiate(explosionEfx, transform.position, Quaternion.identity);

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 5f);

        foreach(Collider2D enemy in enemies)
        {
            enemy.GetComponent<IDamageable>()?.OnDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            StartCoroutine(BulletDestroy(0f));
        }
        else if (collision.CompareTag("Enemy"))
        {
            Attack();
            StartCoroutine(BulletDestroy(0f));
        }
    }
}
