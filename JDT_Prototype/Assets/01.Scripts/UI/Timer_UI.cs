using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_UI : MonoBehaviour
{
    private float time;
    private Text text;

    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    void Update()
    {
        
    }

    void PlayTime()
    {
        time += Time.deltaTime;
        text.text = time.ToString();
    }
}
