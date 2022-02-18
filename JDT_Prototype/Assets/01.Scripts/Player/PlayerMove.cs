using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerAction
{
    [SerializeField] private float speed;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashPlayTime;

    private bool isDash;

    protected override void Start()
    {
        base.Start();

        EventManager.AddEvent_Action("MOVE", Move);
        EventManager.AddEvent_Action("DASH", Dash);
    }

    void Move(float horizontal, float vertical)
    {
        if (!isDash)
        {
            rigid.velocity = new Vector2(horizontal * speed, vertical * speed);
            animator.SetFloat("Horizontal speed", horizontal);
            animator.SetFloat("Vertical speed", vertical);
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