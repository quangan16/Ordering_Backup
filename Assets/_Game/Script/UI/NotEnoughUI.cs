using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotEnoughUI : PopupManager
{
    NotEnoughType type;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] GameObject addCoin;
    [SerializeField] GameObject addHeart;

    public void OnEnable()
    {

    }
    public void Open(NotEnoughType type)
    {
        OnOpen();
        this.type = type;
        if (type == NotEnoughType.Coin)
        {
            title.text = "NOT ENOUGH MONEY";
          
            addCoin.SetActive(true);
            addHeart.SetActive(false);
        }
        else
        {
            title.text = "NOT ENOUGH HEART";
           
            addCoin.SetActive(false);
            addHeart.SetActive(true);
        }
        gameObject.SetActive(true);

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
        WatchAds();
    }


    public void WatchAds()
    {
        AdsAdapter.Instance.ShowRewardedVideo(() =>
            {

                AdsAdapter.LogAFAndFB($"get_coins", "0",
                    "0");
                AddResources();
            }, () =>
            {
                // PanelLoading.Instance.Notify("Watch Failed, Try Again!");
                Debug.Log("Failed to load");
                
            }, 0,
            AdsAdapter.where.get_coins);
    }
    public void AddResources()
    {
        
        if(type == NotEnoughType.Coin)
        {
            DataManager.Instance.AddCoin(100);
            UIManager.Instance.SetCoin();
        }
        else
        {
            UIManager.Instance.challenge.AddHeart();
        }
        Close();
    }
   
}
public enum NotEnoughType
{
    Heart,
    Coin

}
