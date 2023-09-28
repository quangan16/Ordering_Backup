using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ChallengeBought :  ChallegeItemAnimation
{
    public ChallegeItemAnimation bought;
    public bool adWatched = false;
    [SerializeField] GameObject holder;
    private void Start()
    {

    }

    public void OnDisable()
    {
        transform.DOKill();
    }
    
    public void BuyLevel()
    {

        if (DataManager.Instance.GetCoin() >= dataLevel.price)
        {
            DataManager.Instance.AddCoin(-dataLevel.price);
            UIManager.Instance.SetCoin();
            DataManager.Instance.SetLevel(level, Mode.Bought, 0);
            
            gameObject.transform.DOScale(0.0f, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
            {
                holder.SetActive(false);
                gameObject.SetActive(false);
                bought.gameObject.SetActive(true);
                gameObject.SetActive(true);
            });
           
            bought.SetData(dataLevel.rewards);
          
        }
        else
        {
            ChallengeUI.levelID = level;
            UIManager.Instance.OpenNotEnough(NotEnoughType.Coin);
        }
        
        
    }

    public void UnlockByAds()
    {
        DataManager.Instance.SetLevel(level, Mode.Bought, 0);
        UIManager.Instance.SetCoin();
        bought.gameObject.SetActive(true);
        bought.SetData(dataLevel.rewards);
        holder.SetActive(false);
    }
    
    
    public void SetChildren(UnityAction listener)
    {
        bought.playButton.onClick.AddListener(listener);
        bought.level = level;
        bought.SetData(dataLevel.rewards);
    }
}
