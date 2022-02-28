using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : BulletBase
{

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
