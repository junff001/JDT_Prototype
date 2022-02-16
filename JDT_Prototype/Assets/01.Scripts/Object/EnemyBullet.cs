using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private GameObject bullet_real;
    private Collider2D _collider2D;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        bullet_real.SetActive(true);
        _collider2D.enabled = true;

        StartCoroutine(BulletDestroy(5f));
    }

    private void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }

    IEnumerator BulletDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
        bullet_real.SetActive(false);
        _collider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            StartCoroutine(BulletDestroy(0f));
        }
        else if (collision.CompareTag("Player"))
        {
            StartCoroutine(BulletDestroy(0f));
            collision.GetComponent<IDamageable>()?.OnDamage(collision.transform.position, Vector2.zero);
        }
    }
}
