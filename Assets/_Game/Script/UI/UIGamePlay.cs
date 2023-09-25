using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class UIGamePlay : MonoBehaviour,IUIControl
{
    // Start is called before the first frame update
    public TextMeshProUGUI tmp;
    public static bool getHint = false;
    [SerializeField] private List<Button> buttonsList;
    
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI coinTxt;
    
    public void OnEnable()
    {
        
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        GameManager.Instance.OpenGamePlay(GameMode.Normal, DataManager.Instance.GetNormalLevel());
        ActiveButtons();
        ChangeBackground();
    }
    public virtual void Close()
    {
      
        gameObject.SetActive(false);
        
    }
    public void Replay()
    {
        GameManager.Instance.Replay();
        
    }
    public void OpenChallenge()
    {
        UIManager.Instance.OpenChallenge();

    }
    public void NextLevel()
    {
        DataManager.Instance.SetNormalLevel(GameManager.Instance.currentLevel + 1);
        if(DataManager.Instance.GetNormalLevel()>=5)
        {
            Debug.Log("lol");
            AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
                {

                    AdsAdapterAdmob.LogAFAndFB($"skip_level", "0",
                        "0");
                    GameManager.Instance.NextLevel();
                }, () =>
                {
                    // PanelLoading.Instance.Notify("Watch Failed, Try Again!");
                    Debug.Log("Failed to load");

                }, 0,
                AdsAdapterAdmob.where.skip_level);
        }
       
    }
    
    public virtual void CallHint()
    {
        //getHint = true;
        GameManager.Instance.DiscardRandom();
           
    }
    public void SetText(string text)
    {
        tmp.text = text;
    }
    public void SetCoin(int coin)
    {
        this.coinTxt.text = coin.ToString();
    }
    public void OpenShop()
    {
        UIManager.Instance.OpenShop();
    }

    public void BackToMain()
    {
        if (DataManager.Instance.GetNormalLevel() >= 6)
        {
            AdsAdapterAdmob.LogAFAndFB($"back_to_main", "0",
                "0");
            AdsAdapterAdmob.Instance.ShowInterstitial(0, AdsAdapterAdmob.where.back_to_main);
        }
        UIManager.Instance.OpenMain();
        
    }
    
   

    public void ChangeBackground()
    {
        background.sprite = DataManager.Instance.GetBackGround(DataManager.Instance.GetLastBackground()).sprite;
    }
    
    public void DeactiveButtons()
    {
        foreach (var button in buttonsList)
        {
            button.interactable = false;
        }
    }

    public void ActiveButtons()
    {
        foreach (var button in buttonsList)
        {
            button.interactable = true;
        }
    }
    public TextMeshProUGUI GetCoinText()
    {
        return coinTxt;
    }
    



}
