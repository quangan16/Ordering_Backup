using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BouncingEffect : MonoBehaviour
{
    [SerializeField] private float scaleAmount;
    [SerializeField] private float scaleDuration;

    private void OnDisable()
    {
        transform.DOKill();
    }

    private void OnInit()
    {
        gameObject.transform.localScale = Vector3.one;
    }

    private void OnEnable()
    {
        OnInit();
        PlayBouncing();
    }

    private void PlayBouncing()
    {
        gameObject.transform.DOScale(Vector3.one * scaleAmount, scaleDuration).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo)
            .SetDelay(1.0f);
    }
}

