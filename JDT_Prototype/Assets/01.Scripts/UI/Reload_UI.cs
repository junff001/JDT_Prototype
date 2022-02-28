using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Reload_UI : MonoBehaviour
{
    [SerializeField] private GameObject fill_Bar;
    [SerializeField] private SpriteRenderer bar_BackImage;
    [SerializeField] private SpriteRenderer bar_Image;

    void Start()
    {
        OnReloadImage_UI(false);

        EventManager2.AddEvent_Action("ONRELOADIMAGE_UI", OnReloadImage_UI);
        EventManager2.AddEvent_Action("FILLRELOADUIRESET", FillReloadUIReset);
        EventManager2.AddEvent_Function("FILLAMOUNTRELOAD_UI", FillAmountReload_UI);
    }

    Tween FillAmountReload_UI()
    {
        OnReloadImage_UI(true);

        return fill_Bar.transform.DOScaleX(1, EventManager2.TriggerEvent_Float("SHOTGUN_RELOADTIME")).SetEase(Ease.Linear);
    }

    void FillReloadUIReset()
    {
        fill_Bar.transform.localScale = new Vector3(0, 1, 1);
    }

    void OnReloadImage_UI(bool isOn)
    {
        bar_BackImage.enabled = isOn;
        bar_Image.enabled = isOn;
    }

    void OnDestroy()
    {
        EventManager2.RemoveEvent("ONRELOADIMAGE_UI");
        EventManager2.RemoveEvent("FILLAMOUNTRELOAD_UI");
        EventManager2.RemoveEvent("FILLRELOADUIRESET");
    }
}
