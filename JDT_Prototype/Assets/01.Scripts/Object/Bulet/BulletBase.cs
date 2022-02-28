using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BulletBase : MonoBehaviour
{
    public float speed = 20f;
    public float damage;
    public float explodeRadius = 0f;
    protected float currentSpeed;
    public float destroyCount = 10;


    public ParticleSystem explosionEfx;

    private void Awake()
    {
        if(DataManager.weapon == DataManager.Weapon.Bazooka) explodeRadius += 7;
        else if (DataManager.weapon == DataManager.Weapon.shotgun) destroyCount = 1f;
    }

    public virtual void OnEnable()
    {
        currentSpeed = speed;
        StartCoroutine(BulletDestroy(destroyCount));
    }

    public virtual void OnAttack(Collider2D collidedObj)
    {
        collidedObj.GetComponent<IDamageable>()?.OnDamage(damage);
        
        if(explodeRadius > 0)
        {
            ParticleSystem efx = Instantiate(explosionEfx,transform.position,Quaternion.identity);
            efx.transform.localScale = new Vector3(explodeRadius, explodeRadius, explodeRadius);
            List<Collider2D> enemies = Physics2D.OverlapCircleAll(transform.position, explodeRadius).ToList();
            enemies.Remove(collidedObj);
            foreach (Collider2D enemy in enemies) enemy.GetComponent<IDamageable>()?.OnDamage(damage);
        }
    }


    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(transform.up * currentSpeed * Time.deltaTime, Space.World);
    }

    IEnumerator BulletDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        currentSpeed = 0;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            OnAttack(collision);
            StartCoroutine(BulletDestroy(0f));
        }
        else if (collision.CompareTag("Enemy"))
        {
            OnAttack(collision);
            StartCoroutine(BulletDestroy(0f));
        }
    }
}
