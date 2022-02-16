using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Reload_UI : MonoBehaviour
{
    [SerializeField] private GameObject fill_Bar;
    [SerializeField] private SpriteRenderer bar_BackImage;
    [SerializeField] private SpriteRenderer bar_Image;
    [HideInInspector] public float fill_Duration = 0;

    public Tween t;

    void Start()
    {
        Image_Enabled(false);
        //fill_Duration = GameObject.Find("player").GetComponent<PlayerAttack>().reloadTime;
        //EventManager.AddEvent("FILL_AMOUNT", Fill_Amount);
        //EventManager.AddEvent("STOP_RELOAD", StopReload);
        
    }

    void Fill_Amount()
    {
        Image_Enabled(true);
        t = fill_Bar.transform.DOScaleX(1, fill_Duration).SetEase(Ease.Linear).OnComplete(() =>
        {
            Image_Enabled(false);
            fill_Bar.transform.localScale = new Vector3(0, 1, 1);
            //EventManager.TriggerEvent("LOAD_BULLET"); // �Ѿ� ���� UI
        });
    }

    void StopReload()
    {
        t.Complete(false);
        Image_Enabled(false);
        fill_Bar.transform.localScale = new Vector3(0, 1, 1);
    }


    void Image_Enabled(bool isOn)
    {
        bar_BackImage.enabled = isOn;
        bar_Image.enabled = isOn;
    }
}
