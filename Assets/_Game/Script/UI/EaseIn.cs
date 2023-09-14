using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EaseIn : MonoBehaviour
{
    [SerializeField] private RectTransform CoinHolder;
    [SerializeField] private RectTransform LiveHolder;
    [SerializeField] private float moveDuration = 2.0f;
    [SerializeField] private float startTime;
    private float offsetX = 100.0f;
    [SerializeField] private Transform initPos;
    [SerializeField] private Transform desPos;
 
    void OnEnable()
    {
        Reset();
        Invoke("SlideIn", startTime);
    }

    private void OnDisable()
    {
        DOTween.Clear();
    }

    private void OnDestroy()
    {
        DOTween.Clear();
    }

    private void Reset()
    {
        CoinHolder.localPosition = new Vector3(initPos.localPosition.x,
            CoinHolder.localPosition.y, initPos.localPosition.z);
        LiveHolder.localPosition = new Vector3(initPos.localPosition.x,
            LiveHolder.localPosition.y, initPos.localPosition.z);
    
    }
    public void SlideIn()
    {
        
      
        CoinHolder.transform.DOLocalMove(new Vector3(desPos.localPosition.x, CoinHolder.localPosition.y, CoinHolder.localPosition.z)  , moveDuration).SetEase(Ease.OutBack);
        LiveHolder.transform.DOLocalMove(new Vector3(desPos.localPosition.x, LiveHolder.localPosition.y,
            LiveHolder.localPosition.z), moveDuration).SetEase(Ease.OutBack).SetDelay(0.3f);
    }
}
