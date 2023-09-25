using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    [SerializeField] Sprite OutOfTime;
    [SerializeField] Sprite OutOfMove;
    [SerializeField] private Image lossPanel;
    [SerializeField] TextMeshProUGUI AddWhat;
   [SerializeField] private RectTransform FailIcon;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip overTimeSfx;
    [SerializeField] private AudioClip outOfMoveSfx;
    [SerializeField] private RectTransform mainPanel;
    private float scaleDuration = 0.5f;
    private float effectDuration = 4.0f;
    TypeOut type;

    public void OnEnable()
    {
       OnOpen();
       VibrateClock();
    }

    public void OnDisable()
    {
        transform.DOKill();
        FailIcon.DOKill();
    }

    public void AddMoves()
    {
        UIManager.Instance.ShowAds();
        if(type == TypeOut.TimeOut)
        {
            GameManager.timer = 15;

        }
        else
        {
            GameManager.moves = 5;
        }
        Close();
    }
    public void Deny()
    {
        if(GameManager.Instance.gameMode == GameMode.Challenge)
        {
            UIManager.Instance.OpenChallenge();

        }
        else
        {
            UIManager.Instance.OpenGameplay();
        }
        Close();
    }
    public void Close()
    {
        OnClose();


    }
    public void Open(TypeOut type)
    {
        this.type = type;
        OnOpen();
        if(type == TypeOut.TimeOut)
        {
            lossPanel.sprite = OutOfTime;
            lossPanel.gameObject.SetActive(true);
            lossPanel.transform.DOScale(1.0f, scaleDuration).SetEase(Ease.OutBack);
            VibrateClock(); 
            AddWhat.text = "+15 SECS";
            
        }
        else
        {
            lossPanel.sprite = OutOfMove;
            lossPanel.gameObject.SetActive(true);
            lossPanel.transform.DOScale(1.0f, scaleDuration).SetEase(Ease.OutBack);
            AddWhat.text = "+5 MOVES";

        }
        //audioSource.PlayOneShot(outOfMoveSfx);
        mainPanel.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void VibrateClock()
    {
        FailIcon.DOShakeAnchorPos(effectDuration, new Vector3(10.0f, 10.0f, 0), 100, 1);
       
        FailIcon.transform.DORotate(new Vector3(0, 0, 10.0f), 0.01f, RotateMode.Fast).SetEase(Ease.Linear).SetLoops(300, LoopType.Yoyo).OnComplete(()=>
        {
            FailIcon.transform.localEulerAngles = Vector3.zero;
        });
    }

    public void OnOpen()
    {
        //Reset();
        FailIcon.localEulerAngles = new Vector3(0, 0, -10.0f);

        gameObject.SetActive(true);
       // mainPanel.DOScale(1.0f, scaleDuration).SetEase(Ease.OutBack);
    }

    public void OnClose()
    {
        //mainPanel.transform.DOScale(0.0f, scaleDuration).SetEase(Ease.InBack);
            gameObject.SetActive(false);
    }

    void Reset()
    {
        FailIcon.localEulerAngles = new Vector3(0, 0, -10.0f);
       // mainPanel.transform.localScale = Vector3.zero;
    }
}
public enum TypeOut
{
    MoveOut,
    TimeOut
}
