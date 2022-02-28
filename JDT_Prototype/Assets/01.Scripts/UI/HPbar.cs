using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class HPbar : MonoBehaviour
{
    [SerializeField] private GameObject fill_Bar;
    [SerializeField] private float fill_Duration;

    event Action<int> OnHPPersimmon_UI;
    event Action<int> OnHPIncrease_UI;

    void Start()
    {
        OnHPPersimmon_UI = HPPersimmon_UI;
        OnHPIncrease_UI = HPIncrease_UI;

        EventManager.AddEvent("HPPERSIMMON_UI", OnHPPersimmon_UI);
        EventManager.AddEvent("HPINCREASE_UI", OnHPIncrease_UI); 

        //EventManager2.AddEvent_Action("HPPERSIMMON_UI", HPPersimmon_UI);
        //EventManager2.AddEvent_Action("HPINCREASE_UI", HPIncrease_UI);
    }

    void HPPersimmon_UI(int damage)
    {
        fill_Bar.transform.DOScaleX(fill_Bar.transform.localScale.x - (EventManager2.TriggerEvent_Int("MAXHP") / damage), fill_Duration).SetEase(Ease.Linear);
        ClampHP_UI();
    }

    void HPIncrease_UI(int heal)
    {
        fill_Bar.transform.DOScaleX(fill_Bar.transform.localScale.x + (EventManager2.TriggerEvent_Int("MAXHP") / heal), fill_Duration).SetEase(Ease.Linear);
        ClampHP_UI();
    }

    void ClampHP_UI()
    {
        Mathf.Clamp01(fill_Bar.transform.localScale.x);
    }

    void OnDestroy()
    {
        EventManager.RemoveEvent("HPPERSIMMON_UI");
        EventManager.RemoveEvent("HPINCREASE_UI");

        //EventManager2.RemoveEvent("HPPERSIMMON_UI");
        //EventManager2.RemoveEvent("HPINCREASE_UI");
    }
}
