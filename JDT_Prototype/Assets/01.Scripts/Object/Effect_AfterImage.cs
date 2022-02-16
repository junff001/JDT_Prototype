using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Effect_AfterImage : Effect
{
    [SerializeField] SpriteRenderer _sr;

    public void InitAfterImage(Sprite sprite, float lifeTime, Vector2 position)
    {
        this.lifeTime = lifeTime;
        _sr.color = Color.white;
        _sr.sprite = sprite;
        _sr.DOFade(0, this.lifeTime).OnComplete(() => gameObject.SetActive(false));
        transform.position = position;
    }
}
