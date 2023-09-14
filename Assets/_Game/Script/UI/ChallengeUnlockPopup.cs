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
    void Start()
    {
        
    }

    private void OnEnable()
    {
        ResetPos();
        CollapseAnimate();
    }

    // Update is called once per frame
    void Update()
    {
        
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
