using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class ChallengeBought :  ChallegeItemAnimation
{
    public ChallegeItemAnimation bought;
    [SerializeField] GameObject holder;
    private void Start()
    {

    }
    public void BuyLevel()
    {

        if (DataManager.Instance.GetCoin() >= price)
        {
            DataManager.Instance.AddCoin(-price);
            DataManager.Instance.SetLevel(level, Mode.Bought, 0);

        }
        bought.gameObject.SetActive(true);
        bought.SetData(dataLevel.rewards);
        holder.SetActive(false);
        
    }
    public void SetChildren(UnityAction listener)
    {
        bought.playButton.onClick.AddListener(listener);
    }
}
