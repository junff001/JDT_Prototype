using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameOver_UI : MonoBehaviour
{
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        EventManager.AddEvent("GAME_OVER", Gameover);
    }

    void Gameover()
    {
        canvasGroup.DOFade(1, 0.75f);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
