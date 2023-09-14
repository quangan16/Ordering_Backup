using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChallengeUnlockPopup : MonoBehaviour
{
    [SerializeField] private RectTransform topShape;

    [SerializeField] private RectTransform botShape;

    private float DesPosX = 90.0f;
    // Start is called before the first frame update
    private void OnEnable()
    {
        ResetPos();
        CollapseAnimate();
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    { gameObject.SetActive(false); }
    public void OpenChallenge()
    {
        UIManager.Instance.OpenChallenge();
        Close();
    }

 

    void ResetPos()
    {
        topShape.localPosition = new Vector3(0, 20.0f, 0);
    }

    void CollapseAnimate()
    {
        topShape.DOLocalMoveX(DesPosX, 0.7f).SetEase(Ease.OutBounce);
        botShape.DOLocalMoveX(-DesPosX, 0.7f).SetEase(Ease.OutBounce);
    }
}
