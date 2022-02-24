using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HPbar : MonoBehaviour
{
    [SerializeField] private GameObject fill_Bar;
    [SerializeField] private float fill_Duration;

    void Start()
    {
        EventManager.AddEvent_Action("HPPERSIMMON_UI", HPPersimmon_UI);
        EventManager.AddEvent_Action("HPINCREASE_UI", HPIncrease_UI);
    }

    void HPPersimmon_UI(int damage)
    {
        fill_Bar.transform.DOScaleX(fill_Bar.transform.localScale.x - (EventManager.TriggerEvent_Int("MAXHP") / damage), fill_Duration).SetEase(Ease.Linear);
        ClampHP_UI();
    }

    void HPIncrease_UI(int heal)
    {
        fill_Bar.transform.DOScaleX(fill_Bar.transform.localScale.x + (EventManager.TriggerEvent_Int("MAXHP") / heal), fill_Duration).SetEase(Ease.Linear);
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
    }
}
