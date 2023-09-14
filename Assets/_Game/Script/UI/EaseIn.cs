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
    private float offset = 6.0f;
    private float initialPosX = 8.23f;
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
        CoinHolder.transform.position = new Vector3(initialPosX,
            CoinHolder.transform.position.y,0);
        LiveHolder.transform.position = new Vector3(initialPosX,
            LiveHolder.transform.position.y,0);
    
    }
    public void SlideIn()
    {
        
      
        CoinHolder.transform.DOMoveX(CoinHolder.transform.position.x - offset, moveDuration).SetEase(Ease.OutBack);
        LiveHolder.transform.DOMoveX(LiveHolder.transform.position.x - offset, moveDuration).SetEase(Ease.OutBack).SetDelay(0.3f);
    }
}
