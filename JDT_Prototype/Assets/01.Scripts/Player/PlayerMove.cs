using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerAction
{
    [SerializeField] private float speed;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDelayTime;

    protected override void Start()
    {
        base.Start();

        EventManager.AddEvent_Action("MOVE", Move);
        EventManager.AddEvent_Action("DASH", Dash);
    }

    void Move(float horizontal, float vertical)
    {
        rigid.velocity = new Vector2(horizontal * speed, vertical * speed);
    }

    void Dash()
    {
        if (rigid.velocity.sqrMagnitude > 0.1f)
        {
            Vector2 dashAfterPos;
            dashAfterPos = rigid.position + rigid.velocity.normalized * dashDistance;
            rigid.velocity = rigid.velocity.normalized * dashDistance;

            EventManager.TriggerEvent_Action("RELOAD");

            if (rigid.position == dashAfterPos)
            {
                AfterImageManager.isOnAfterEffect = false;
            }

            if (!AfterImageManager.isOnAfterEffect)
            {
                AfterImageManager.isOnAfterEffect = true;
            }

            StartCoroutine(Dash_Delay());
        }
    }

    IEnumerator Dash_Delay()
    {
        yield return new WaitForSeconds(dashDelayTime);
    }

    void OnDestroy()
    {
        EventManager.RemoveEvent("MOVE");
        EventManager.RemoveEvent("DASH");
    }
}