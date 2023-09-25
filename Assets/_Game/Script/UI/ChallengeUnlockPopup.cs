using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChallengeUnlockPopup : PopupManager
{
    [SerializeField] private RectTransform topShape;

    [SerializeField] private RectTransform botShape;

    private float DesPosX = 90.0f;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        Reset();
        OnOpen();
    }
    public void Open()
    {
       
        OnOpen();
    }

    public void Close()
    {
        OnClose();
        GameManager.Instance.StartCountDown();


    }
    public void OpenChallenge()
    {
        OnClose();
        UIManager.Instance.OpenChallengeAndScroll();
     
    }

    public new void  OnOpen()
    {
        gameObject.SetActive(true);
        mainPanel.transform.DOScale(1.0f, scaleDuration).SetEase(Ease.OutBack).OnComplete(() => CollapseAnimate());
    }

   

  

    void Reset()
    {
        mainPanel.transform.localScale = Vector3.zero;
        topShape.localPosition = new Vector3(0, 20.0f, 0);
        botShape.localPosition = new Vector3(0, -20.0f, 0);
    }

    void CollapseAnimate()
    {
        topShape.DOLocalMoveX(DesPosX, 0.7f).SetEase(Ease.OutBounce);
        botShape.DOLocalMoveX(-DesPosX, 0.7f).SetEase(Ease.OutBounce);
    }
}
