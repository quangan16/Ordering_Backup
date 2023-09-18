using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ZoomOutPopup : MonoBehaviour
{
    private float scaleDuration = 0.5f;
    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        EmergeEaseOutBack();
    }

    private void OnDisable()
    {
        transform.DOKill();
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

    void EmergeEaseOutBack()
    {
        transform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.OutBack);
    }
}
