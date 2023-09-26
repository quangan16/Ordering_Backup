using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotEnoughUI : PopupManager
{
    NotEnoughType type;
    [SerializeField] private RectTransform coinPanel;
    [SerializeField] private RectTransform livePanel;

    public void OnEnable()
    {

    }
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
        AddResources();
    }
    public void AddResources()
    {
        
        if(type == NotEnoughType.Coin)
        {
           
            AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
                {
                    
                    AdsAdapterAdmob.LogAFAndFB($"get_coin", "0",
                        "0");
                    DataManager.Instance.AddCoin(100);
                    UIManager.Instance.SetCoin();
                }, () =>
                {
                    StartCoroutine(GameManager.Instance.CheckInternetConnection()) ;
                    if (GameManager.Instance.HasInternet == false)
                    {
                        UIManager.Instance.ShowInternetPopUp();
                    }
                    else
                    {
                        UIManager.Instance.ShowAdsNotification();
                    }
                    // PanelLoading.Instance.Notify("Watch Failed, Try Again!");
                    // Debug.Log("Failed to load");

                }, 0,
                AdsAdapterAdmob.where.get_coin);
            
           
        }
        else
        {
            AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
                {

                    AdsAdapterAdmob.LogAFAndFB($"get_live", "0",
                        "0");
                    UIManager.Instance.challenge.AddHeart();
                }, () =>
                {
                    // PanelLoading.Instance.Notify("Watch Failed, Try Again!");
                    Debug.Log("Failed to load");

                }, 0,
                AdsAdapterAdmob.where.get_live);
          
        }
        Close();
    }
   
}
public enum NotEnoughType
{
    Heart,
    Coin

}
