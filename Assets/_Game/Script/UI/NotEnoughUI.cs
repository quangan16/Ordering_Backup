using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NotEnoughUI : PopupManager
{
    NotEnoughType type;
    private int itemID;
    [SerializeField] private RectTransform coinPanel;
    [SerializeField] private RectTransform livePanel;
    [SerializeField] Button unlockAds;
    
    public void Open(NotEnoughType type)
    {
        
        this.type = type;
        if (type == NotEnoughType.Coin)
        {
            mainPanel = coinPanel;
        }
        else
        {
            mainPanel = livePanel;
           
        }
        mainPanel.gameObject.SetActive(true);
        gameObject.SetActive(true);
        OnOpen();
       
    }
    public void Close()
    {
        OnClose();
    }
    public void DenyBtn()
    {
        Close();
    }

    public void OnAccept()
    {
        UnlockItem();
        
    }
    public void UnlockItem()
    {
        
        if(type == NotEnoughType.Coin)
        {
            if (UIManager.Instance.current is ShopGUI)
            {
                AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
                    {

                        AdsAdapterAdmob.LogAFAndFB($"unlock_item_by_ads", "0",
                            "0");
                        ShopGUI.UnlockItemWithAds();

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
                    AdsAdapterAdmob.where.unlock_item_by_ads);
            }
            
            else if (UIManager.Instance.current is ChallengeUI)
            {
                AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
                    {
                        DataManager.Instance.SetLevel(ChallengeUI.levelID, Mode.Bought, 0);
                        AdsAdapterAdmob.LogAFAndFB($"unlock_challenge_by_ads", "0",
                            "0");
                        //UIManager.Instance.ReloadChallenge();
                        ChallengeUI.challengeBought.ChangeMode();
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
                        // PanelLoading.Instance.Notify("Watch Failed, Try Again!");

                    }, 0,
                    AdsAdapterAdmob.where.unlock_challenge_by_ads);
            }
            else if (UIManager.Instance.current is UIGamePlay)
            {
                AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
                    {

                        AdsAdapterAdmob.LogAFAndFB($"unlock_item_by_ads", "0",
                            "0");
                        

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
                    AdsAdapterAdmob.where.unlock_item_by_ads);
            }
            
           
        }
        else
        {
            AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
                {

                    AdsAdapterAdmob.LogAFAndFB($"get_live", "0",
                        "0");
                    UIManager.Instance.challenge.AddHeart(3);
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
                AdsAdapterAdmob.where.get_live);
          
        }
        Close();
    }
   public void AddListener(UnityAction listener)
    {
        unlockAds.onClick.AddListener(listener);
    }

}
public enum NotEnoughType
{
    Heart,
    Coin

}
