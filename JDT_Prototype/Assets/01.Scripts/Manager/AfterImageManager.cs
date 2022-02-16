using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AfterImageManager : MonoBehaviour
{
    [SerializeField] GameObject afterImagePrefab = null;
    [SerializeField] Transform afterImageParent = null;
    [SerializeField] private float afterEffectInterval = 1f;
    [SerializeField] private float afterImageLifeTime = 1f;

    SpriteRenderer _sr = null;

    private float lastAfterEffectTime = 0f;

    public static bool isOnAfterEffect = false;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        PoolManager.CreatePool<Effect_AfterImage>(afterImagePrefab, afterImageParent);
    }

    private void Update()
    {
        if(isOnAfterEffect)
        {
            if(lastAfterEffectTime + afterEffectInterval <= Time.time)
            {
                Effect_AfterImage curAfterImage = PoolManager.GetItem<Effect_AfterImage>();
                curAfterImage.InitAfterImage(_sr.sprite, afterImageLifeTime, transform.position);
                lastAfterEffectTime = Time.time;
            }
        }
    }

    private void OnDestroy()
    {
        isOnAfterEffect = false;
    }
}
