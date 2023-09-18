using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FadePopup : MonoBehaviour
{
    public Transform main;
    
    [SerializeField] private CanvasGroup challengeCanvas;
    private float fadeOutDuration = 0.2f;
    private void Awake()
    {
    }

    public void OnDisable()
    {
        challengeCanvas.DOKill();

        challengeCanvas.transform.DOKill();
    }

    public void OnDestroy()
    {
        challengeCanvas.DOKill();
        challengeCanvas.transform.DOKill(); 
    }
    public void Show(object data)
    {
        challengeCanvas.transform.localPosition = new Vector3(0, 30.0f);
        challengeCanvas.transform.DOLocalMove(new Vector3(0, 0, 0), 0.7f);
        challengeCanvas.DOFade(1f, fadeOutDuration);
    }

    public void Hide()
    {

        challengeCanvas.DOFade(0f, fadeOutDuration).OnComplete(() =>
        {
            UIManager.Instance.OpenMain();
            
        });
    }

}
