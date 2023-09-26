using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class VictoryUIAnim : MonoBehaviour
{
    [SerializeField] Image titleAlpha;
    [SerializeField] private GameObject adsButton;
    [SerializeField] private CanvasGroup victoryCanvas;
    [SerializeField] private Image CoinBanner;
    
    private float fadeInDuration = 0.5f;


    void OnEnable()
    {
        // CoinBanner.transform.SetParent(victoryCanvas.transform.GetChild(1));
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
        // Vector3 initCoinPos = new Vector3(-212.7f, -75.0f, 0.0f);
        // CoinBanner.transform.localPosition = initCoinPos;
        victoryCanvas.alpha = 0;
        victoryCanvas.transform.localPosition = new Vector3(0, -30.0f);
        titleAlpha.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
        adsButton.transform.localScale = Vector3.one;

    }

    void Show()
    {

        victoryCanvas.transform.DOLocalMove(new Vector3(0, 0, 0), 0.7f);
        victoryCanvas.DOFade(1f, fadeInDuration);
        CoinBanner.DOFade(1, 0.5f).SetDelay(0.5f);
        CoinBanner.transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOFade(1, 0.5f).SetDelay(0.5f);
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