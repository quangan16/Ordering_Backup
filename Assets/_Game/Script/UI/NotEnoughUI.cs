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
            title.text = "Not enough money";
          
            addCoin.SetActive(true);
            addHeart.SetActive(false);
        }
        else
        {
            title.text = "Not enough heart";
           
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
                    // PanelLoading.Instance.Notify("Watch Failed, Try Again!");
                    Debug.Log("Failed to load");

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
