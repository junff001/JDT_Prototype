using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerAction
{
    [SerializeField] private float speed;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDelayTime;

    private bool isDash = false;

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
        }   
    }

    void Dash()
    {
        if (rigid.velocity.sqrMagnitude > 0.1f)
        {
            isDash = true;
            //float speed = this.speed;

            //Vector2 dashAfterPos = rigid.velocity.normalized * dashDistance;

            rigid.velocity = rigid.velocity.normalized * dashDistance;

            //this.speed = 0;

            //EventManager.TriggerEvent_Action("RELOAD");

            //if (rigid.position.x >= dashAfterPos.x || rigid.position.y >= dashAfterPos.y)
            //{
            //    this.speed = speed;
            //    //AfterImageManager.isOnAfterEffect = false;
            //}

            //if (!AfterImageManager.isOnAfterEffect)
            //{
            //    AfterImageManager.isOnAfterEffect = true;
            //}

            StartCoroutine(Dash_Delay());
        }
    }

    IEnumerator Dash_Delay()
    {
        yield return new WaitForSeconds(dashDelayTime);
        isDash = false;
    }

    void OnDestroy()
    {
        EventManager.RemoveEvent("MOVE");
        EventManager.RemoveEvent("DASH");
    }
}