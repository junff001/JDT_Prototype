using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMove : Player_Action
{
    public float dashPlayTime;
    public float dashDelayTime;
    public float reboundingTime;

    public float dashDistance = 5f;

    private float moveX;
    private float moveY;

    public static bool isDead = false;
    public static bool isDashing = false;
    private bool isRebounding = false;
    private bool canDash = true;

    [SerializeField] private float speed;

    WaitForSeconds ws_dashPlay;
    WaitForSeconds ws_dashDelay;

    PlayerAttack pa;

    protected override void Start() 
    {
        base.Start();
        ws_dashPlay = new WaitForSeconds(dashPlayTime);
        ws_dashDelay = new WaitForSeconds(dashDelayTime);
        pa = GetComponent<PlayerAttack>();
    }

    protected override void Update()
    {
        if (isDead) return;

        Move();
        if (Input.GetMouseButtonDown(1))
        {
            if (isDashing || !canDash) return;
            StartCoroutine(Dash());
        }
    }

    void Move()
    {
        if (isDashing || isRebounding) return;

        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        rigid.velocity = new Vector3(moveX * speed, moveY * speed, 0);
    }

    IEnumerator Dash()
    {
        if (rigid.velocity.sqrMagnitude < 0.1f) yield break;

        EventManager.TriggerEvent("STOP_RELOAD");
        pa.DashReload();
        rigid.velocity = rigid.velocity.normalized * dashDistance;

        isDashing = true;
        AfterImageManager.isOnAfterEffect = true;
        canDash = false;
        GameManager.Instance.OnReload.Invoke();
        yield return ws_dashPlay;
        AfterImageManager.isOnAfterEffect = false;
        isDashing = false;
        yield return ws_dashDelay;
        canDash = true;
    }
}