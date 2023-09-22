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
    public TextMeshProUGUI coin;
    [SerializeField] private Image background;

    public void OnEnable()
    {
        
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        GameManager.Instance.OpenGamePlay(GameMode.Normal, DataManager.Instance.GetNormalLevel());
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
        // if(DataManager.Instance.GetNormalLevel()>=5)
        // {
        //     AdsAdapter.Instance.ShowInterstitial(0, AdsAdapter.where.next_level);
        //
        // }
        GameManager.Instance.NextLevel();
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
        this.coin.text = coin.ToString();
    }
    public void OpenShop()
    {
        UIManager.Instance.OpenShop();
    }

    public void BackToMain()
    {
        // AdsAdapter.Instance.ShowInterstitial(0, AdsAdapter.where.back_to_main);
        UIManager.Instance.OpenMain();
        
    }
    
   

    public void ChangeBackground()
    {
        background.sprite = DataManager.Instance.GetBackGround(DataManager.Instance.GetLastBackground()).sprite;
    }
    




}
