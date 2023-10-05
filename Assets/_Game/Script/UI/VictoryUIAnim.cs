using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class VictoryUIAnim : MonoBehaviour
{
    [SerializeField] Image titleAlpha;
    [SerializeField] private GameObject adsButton;
    [SerializeField] private CanvasGroup canvas;
    private float fadeInDuration = 0.5f;


    void OnEnable()
    {

    InitSetup();
        Show();

        BoucingButton();
    }

    private void OnDisable()
    {
        DOTween.Clear();
    }

    void InitSetup()
    {
        canvas.alpha = 0.0f;
        titleAlpha.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
        adsButton.transform.localScale = Vector3.one;

    }

    void Show()
    {
        canvas.DOFade(1, 0.4f);
        titleAlpha.DOFade(1, 0.7f);
        titleAlpha.transform.DOScale(1, 1.0f).SetEase(Ease.OutBounce).SetDelay(0.5f);
    }

    void BoucingButton()
    {
        Sequence tweenSequence = DOTween.Sequence();
        tweenSequence.Append(adsButton.transform.DOScale(1.1f, 1.5f).SetEase(Ease.OutElastic)).Append(adsButton
            .transform
            .DOScale(1.0f, 0.2f)).SetDelay(1.0f).SetLoops(-1).OnComplete(()=>{tweenSequence.Kill();});


        tweenSequence.Play();
    }


}