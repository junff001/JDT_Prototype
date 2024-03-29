using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        EventManager.TriggerEvent("MOVE", Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // 이동
        //EventManager2.TriggerEvent_Action("MOVE", Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // 이동

        if (Input.GetMouseButtonDown(1)) // 대쉬 (우클릭)
        {
            EventManager.TriggerEvent("DASH");
            //EventManager2.TriggerEvent_Action("DASH");
        }
        if (Input.GetMouseButtonDown(0)) // 공격 (좌클릭)
        {
            EventManager2.TriggerEvent_Action("SHOOT");
            //EventManager2.TriggerEvent_Action("ATTACK");
        }
        if (Input.GetButtonDown("Reload")) // 재장전 (R 키)
        {
            EventManager2.TriggerEvent_Action("RELOAD");
            //EventManager2.TriggerEvent_Action("RELOAD");
        }
    }
}
