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

        EventManager.AddEvent_Action("ONRELOADIMAGE_UI", OnReloadImage_UI);
        EventManager.AddEvent_Action("FILLRELOADUIRESET", FillReloadUIReset);
        EventManager.AddEvent_Function("FILLAMOUNTRELOAD_UI", FillAmountReload_UI);
    }

    Tween FillAmountReload_UI()
    {
        OnReloadImage_UI(true);

        return fill_Bar.transform.DOScaleX(1, EventManager.TriggerEvent_Float("RELOADTIME")).SetEase(Ease.Linear);
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
        EventManager.RemoveEvent("ONRELOADIMAGE_UI");
        EventManager.RemoveEvent("FILLAMOUNTRELOAD_UI");
        EventManager.RemoveEvent("FILLRELOADUIRESET");
    }
}
