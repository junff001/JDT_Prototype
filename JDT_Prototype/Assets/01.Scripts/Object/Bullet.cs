using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private float currentSpeed;

    [SerializeField] private GameObject bullet_real;
    [SerializeField] private GameObject particle;
    private Collider2D _collider2D;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnEnable()                                                                                                                                     
    {
        currentSpeed = speed;
        bullet_real.SetActive(true);
        particle.SetActive(true);
        _collider2D.enabled = true;

        StartCoroutine(BulletDestroy(0.2f));
        StartCoroutine(ParticleDestroy(0.5f));
    }

    private void Update()
    {
        transform.Translate(transform.up * currentSpeed * Time.deltaTime, Space.World);
    }

    IEnumerator BulletDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        currentSpeed = 0;
        bullet_real.SetActive(false);
        _collider2D.enabled = false;

        ParticleSystem[] particleSystems = particle.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Stop();
        }
    }

    IEnumerator ParticleDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        particle.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            StartCoroutine(BulletDestroy(0f));
            StartCoroutine(ParticleDestroy(0.5f));
        }
        else if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamageable>()?.OnDamage(collision.transform.position, Vector2.zero);
            StartCoroutine(BulletDestroy(0f));
            StartCoroutine(ParticleDestroy(0.5f));
        }
    }
}
