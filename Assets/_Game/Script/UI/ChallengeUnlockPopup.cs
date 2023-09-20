using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChallengeUnlockPopup : MonoBehaviour
{
    [SerializeField] private RectTransform topShape;

    [SerializeField] private RectTransform botShape;

    [SerializeField] private GameObject mainPanel;

    private float DesPosX = 90.0f;

    private float scaleDuration = 0.5f;
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

    }
    public void OpenChallenge()
    {
        UIManager.Instance.OpenChallenge();
       
    }

    public void OnOpen()
    {
        gameObject.SetActive(true);
        mainPanel.transform.DOScale(1.0f, scaleDuration).SetEase(Ease.OutBack).OnComplete(() => CollapseAnimate());
    }
    public void OnClose()
    {
        mainPanel.transform.DOScale(0.0f, scaleDuration).SetEase(Ease.InBack)
            .OnComplete(() => gameObject.SetActive(false));
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
