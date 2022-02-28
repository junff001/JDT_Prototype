using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerAction
{
    [SerializeField] private float speed;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashPlayTime;

    private bool isDash;
    private bool isMove;
    private Vector2 idleDiration;

    protected override void Start()
    {
        base.Start();

       
    }

    public void Init()
    {
        EventManager.AddEvent_Action("MOVE", Move);
        EventManager.AddEvent_Action("DASH", Dash);
    }

    protected override void Update()
    {
        base.Update();

        isMove = rigid.velocity.sqrMagnitude != 0;
        animator.SetBool("IsMove", isMove); // Idle, Movement 변환
        if (isMove)
        {
            animator.SetFloat("Horizontal speed", rigid.velocity.x);
            animator.SetFloat("Vertical speed", rigid.velocity.y);
            idleDiration = new Vector2(rigid.velocity.x, rigid.velocity.y);
        }
        else
        {
            animator.SetFloat("Horizontal speed", idleDiration.x);
            animator.SetFloat("Vertical speed", idleDiration.y); 
        }
    }

    void Move(float horizontal, float vertical)
    {
        if (!isDash)
        {
            rigid.velocity = new Vector2(horizontal * speed, vertical * speed);
        }   
    }

    void Dash()
    {
        if (rigid.velocity.sqrMagnitude > 0.1f)
        {
            isDash = true;
            rigid.velocity = rigid.velocity.normalized * dashDistance;
            EventManager.TriggerEvent_Action("RELOAD");
            AfterImageManager.isOnAfterEffect = true;

            StartCoroutine(DashEnd());
        }
    }

    IEnumerator DashEnd()
    {
        yield return new WaitForSeconds(dashPlayTime); // 0.5초 뒤에 대쉬가 끝남
        AfterImageManager.isOnAfterEffect = false;
        isDash = false;
    }

    void OnDestroy()
    {
        EventManager.RemoveEvent("MOVE");
        EventManager.RemoveEvent("DASH");
    }
}