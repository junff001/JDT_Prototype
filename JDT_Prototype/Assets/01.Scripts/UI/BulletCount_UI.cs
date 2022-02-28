using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCount_UI : MonoBehaviour
{
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        EventManager2.AddEvent_Action("CURRENTBULLETCOUNT_UI", CurrentBulletCount_UI);
    }

    void CurrentBulletCount_UI(int bulletCount)
    {
        text.text = bulletCount.ToString();
    }

    void OnDestroy()
    {
        EventManager2.RemoveEvent("CURRENTBULLETCOUNT_UI");
    }
}
