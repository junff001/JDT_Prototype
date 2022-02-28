using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class GameOver_UI : MonoBehaviour
{
    CanvasGroup canvasGroup;
    event Action OnGameOver;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        OnGameOver = GameOver;
        EventManager.AddEvent("GAME_OVER", OnGameOver);
        //EventManager2.AddEvent_Action("GAME_OVER", Gameover);
    }

    void GameOver()
    {
        canvasGroup.DOFade(1, 0.75f);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
