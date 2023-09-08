using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject multiplierBar;
    // [SerializeField] private Transform startPoint;
    [SerializeField] private Transform  endPoint;
    [SerializeField] private Transform startPoint;

    [SerializeField] private float slideDuration;
    [SerializeField] private TextMeshProUGUI adsCOinTxt;
    [SerializeField] private TextMeshProUGUI normalCOinTxt;
    Vector2 pos;
    private float multiplierBarLength;

    void Start()
    {
        // multiplierBarLength = MultiplierBar.GetComponent<RectTransform>().rect.width;
    }

    private void OnEnable()
    {
       //SlideAndBounce();
    }

    private void OnDisable()
    {
        transform.DOPause();
    }


    public void SlideAndBounce()
    {
        //DOTween.Clear();
        var startPositionX = gameObject.GetComponent<RectTransform>().rect.x;
        var endPositionX = endPoint.GetComponent<RectTransform>().rect.x;
        transform.DOLocalMoveX(endPoint.position.x - transform.localPosition.x, slideDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        //transform.DOMoveX(endPoint.position.x-pos.x, slideDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        //transform.DOMoveX(endPositionX-startPositionX, slideDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);



    }
    public void Move()
    {
        transform.position = startPoint.position;
        transform.DOMove(endPoint.position,slideDuration).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out TextMeshProUGUI text))
        {
            // adsCOinTxt = normalCOinTxt.text * 
        }
        


    }
}
