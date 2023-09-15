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

    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float slideDuration;
    [SerializeField] public TextMeshProUGUI adsCoinTxt;
    [SerializeField] public TextMeshProUGUI achievedCoinTxt;
    [SerializeField] private WinUI winUI;
    private float multiplierBarLength;



    void Start()
    {
    }

    private void OnEnable()
    {
        ResetPosition();
        SlideAndBounce();
    }

    private void ResetPosition()
    {
       transform.position = startPoint.position;
    }
    // private void OnDisable()
    // {
    //     transform.DOPause();
    // }

    void SlideAndBounce()
    {     
            transform.DOLocalMoveX(endPoint.position.x - transform.localPosition.x, slideDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if (winUI.ButtonClicked == false)
        // {
            if (other.CompareTag("Multiplier") && other.TryGetComponent(out TextMeshProUGUI text))
            {
                float targetTextSize = 50;
                float scaleDuration = 0.3f;
                var multiplier = text.text[1] - '0';
                adsCoinTxt.text = (int.Parse(achievedCoinTxt.text) * multiplier).ToString();
                DOTween.To(() => text.fontSize, x => text.fontSize = x, targetTextSize, scaleDuration);

            }
        // }
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // if (winUI.ButtonClicked == false)
        // {
            float normalTextSize = 39;
            float scaleDuration = 0.3f;
            if (other.CompareTag("Multiplier") && other.TryGetComponent(out TextMeshProUGUI text))
            {
                DOTween.To(() => text.fontSize, x => text.fontSize = x, normalTextSize, scaleDuration);

            }
        // }
    }

    // public void Move()
    // {
    //     transform.position = startPoint.position;
    //     transform.DOMove(endPoint.position, slideDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    // }



}