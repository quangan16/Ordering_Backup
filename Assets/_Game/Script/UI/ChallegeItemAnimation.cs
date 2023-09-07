using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChallegeItemAnimation : MonoBehaviour
{
    private float scaleDuration = 0.5f;
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        EmergeEaseOutBack();
    }

    void EmergeEaseOutBack()
    {
        transform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.OutBack);
    }
}
