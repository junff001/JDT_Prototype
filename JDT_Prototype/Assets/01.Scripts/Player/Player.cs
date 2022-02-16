using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigid { get; private set; }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            EnemyBullet bullet = collision.GetComponent<EnemyBullet>();

            if (bullet != null)
            {
                if (!PlayerMove.isDashing)
                {
                    gameObject.SetActive(false);

                    PlayerMove.isDead = true;
                    EventManager.TriggerEvent("GAME_OVER");
                }
            }
        }
    }


}
