using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EaseIn : MonoBehaviour
{
    [SerializeField] private GameObject CoinHolder;
    [SerializeField] private GameObject LiveHolder;
    [SerializeField] private float moveDuration = 2.0f;
    [SerializeField] private float startTime;
    private float initialOffsetPosition = 5.0f;
    void OnEnable()
    {
      
        Invoke("SlideIn", startTime);
    }

    public void SlideIn()
    {
        CoinHolder.transform.position = new Vector2(CoinHolder.transform.position.x + initialOffsetPosition,
            CoinHolder.transform.position.y);
        LiveHolder.transform.position = new Vector2(LiveHolder.transform.position.x + initialOffsetPosition,
            LiveHolder.transform.position.y);
        CoinHolder.transform.DOMoveX(CoinHolder.transform.position.x - initialOffsetPosition, moveDuration).SetEase(Ease.OutBack);
        LiveHolder.transform.DOMoveX(LiveHolder.transform.position.x - initialOffsetPosition, moveDuration).SetEase(Ease.OutBack).SetDelay(0.3f);
    }
}
