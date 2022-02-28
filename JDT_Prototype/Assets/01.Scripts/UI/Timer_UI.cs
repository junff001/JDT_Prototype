using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;

public class Timer_UI : MonoBehaviour
{
    float time;
    string minute;
    string second;
    string emptyZero;

    private Text timer;

    void Start()
    {
        timer = GetComponentInChildren<Text>();
    }

    void Update()
    {
        time += Time.deltaTime;
        minute = ((int)time / 60).ToString();
        second = ((int)time % 60).ToString();
        emptyZero = "0";
        if (second.Length >= 2)
        {
            emptyZero = null;
        }        
        timer.text = minute + ":" + emptyZero + second;
    }
}
