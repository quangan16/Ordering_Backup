using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoseUI : PopupManager
{
    [SerializeField] private RectTransform overTimePanel;
    [SerializeField] private RectTransform outOfMovePanel;
    [SerializeField] private RectTransform clockIcon;
    [SerializeField] private Image controllerIcon;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip overTimeSfx;
    [SerializeField] private AudioClip outOfMoveSfx;
    private float effectDuration = 4.0f;
    TypeOut type;


    void OnEnable()
    {
        OnOpen();
    }
    public void OnDisable()
    {
        overTimePanel.gameObject.SetActive(false);
        outOfMovePanel.gameObject.SetActive(false);
        transform.DOKill();
        mainPanel.transform.DOKill();
    }

    public void AddMoves()
    {
        UIManager.Instance.ShowAds();
        if(type == TypeOut.TimeOut)
        {
            if(UIManager.Instance.current is UIBossGameplay )
            AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
                {

                    AdsAdapterAdmob.LogAFAndFB($"boss_get_more_time_level_" + GameManager.Instance.currentLevel + 1, "0",
                        "0");
                    GameManager.timer = 15;
                    UIManager.Instance.SetCoin();
                }, () =>
                {
                    StartCoroutine(GameManager.Instance.CheckInternetConnection());
                    if (GameManager.Instance.HasInternet == false)
                    {
                        UIManager.Instance.ShowInternetPopUp();
                    }
                    else
                    {
                        UIManager.Instance.ShowAdsNotification();
                    }
                   

                }, 0,
                AdsAdapterAdmob.where.boss_get_more_time_level);

            else if (UIManager.Instance.current is UIChallengeGameplay)
                AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
                    {

                        AdsAdapterAdmob.LogAFAndFB($"challenge_get_more_time_level_" + GameManager.Instance.currentLevel + 1,
                            "0",
                            "0");
                        GameManager.timer = 15;
                        UIManager.Instance.SetCoin();
                    }, () =>
                    {
                        StartCoroutine(GameManager.Instance.CheckInternetConnection());
                        if (GameManager.Instance.HasInternet == false)
                        {
                            UIManager.Instance.ShowInternetPopUp();
                        }
                        else
                        {
                            UIManager.Instance.ShowAdsNotification();
                        }


                    }, 0,
                    AdsAdapterAdmob.where.challenge_get_more_time_level);
        }
        else
        {
            AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
                {

                    AdsAdapterAdmob.LogAFAndFB($"boss_get_more_moves_level_" + GameManager.Instance.currentLevel + 1, "0",
                        "0");
                    GameManager.moves = 5;
                    UIManager.Instance.SetCoin();
                }, () =>
                {
                    StartCoroutine(GameManager.Instance.CheckInternetConnection());
                    if (GameManager.Instance.HasInternet == false)
                    {
                        UIManager.Instance.ShowInternetPopUp();
                    }
                    else
                    {
                        UIManager.Instance.ShowAdsNotification();
                    }


                }, 0,
                AdsAdapterAdmob.where.boss_get_more_moves_level);
           
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
       
        if(type == TypeOut.TimeOut)
        {
            mainPanel = overTimePanel;
            mainPanel.gameObject.SetActive(true);
            VibrateClock();
        }
        else
        {
            mainPanel = outOfMovePanel;
            mainPanel.gameObject.SetActive(true);
        }

        OnOpen();
        //audioSource.PlayOneShot(outOfMoveSfx);
        gameObject.SetActive(true);
    }

    public void VibrateClock()
    {
        clockIcon.DOShakeAnchorPos(effectDuration, new Vector3(10.0f, 10.0f, 0), 100, 1);

        clockIcon.transform.DORotate(new Vector3(0, 0, 10.0f), 0.01f, RotateMode.Fast).SetEase(Ease.Linear).SetLoops(300, LoopType.Yoyo).OnComplete(()=>
        {
            clockIcon.transform.localEulerAngles = Vector3.zero;
        });
    }

   

   

    void Reset()
    {
       
    }
}
public enum TypeOut
{
    MoveOut,
    TimeOut
}
