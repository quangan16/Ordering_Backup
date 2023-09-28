using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ChallengeBought :  ChallegeItemAnimation
{
    public ChallegeItemAnimation bought;
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
            
            gameObject.transform.DOScale(0.0f, 0.15f).SetEase(Ease.InBack).OnComplete(() =>
            {
               ChangeMode();
            });
           
            
          
        }
        else
        {
            //ChallengeUI.levelID = level;
            ChallengeUI.challengeBought = this;
            UIManager.Instance.OpenNotEnough(NotEnoughType.Coin);
        }
        
        
    }
    public void ChangeMode()
    {
        gameObject.transform.DOScale(1f, 0.15f).SetEase(Ease.InBack);
        holder.SetActive(false);
        bought.gameObject.SetActive(true);
        bought.SetData(dataLevel.rewards);
    }    

    
    
    
    public void SetChildren(UnityAction listener)
    {
        bought.playButton.onClick.AddListener(listener);
        bought.level = level;
        bought.SetData(dataLevel.rewards);
    }
}
