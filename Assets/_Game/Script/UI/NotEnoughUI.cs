using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotEnoughUI : MonoBehaviour
{
    NotEnoughType type;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;
    [SerializeField] GameObject addCoin;
    [SerializeField] GameObject addHeart;
    public void Open(NotEnoughType type)
    {
        this.type = type;
        if (type == NotEnoughType.Coin)
        {
            title.text = "NOT ENOUGH MONEY";
            content.text = "Watch ADs to earn more coins";
            addCoin.SetActive(true);
            addHeart.SetActive(false);
        }
        else
        {
            title.text = "NOT ENOUGH HEART";
            content.text = "Watch ADs to earn 1 heart";
            addCoin.SetActive(false);
            addHeart.SetActive(true);
        }
        gameObject.SetActive(true);

    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void DenyBtn()
    {
        Close();
    }
    public void Accept()
    {
        
        if(type == NotEnoughType.Coin)
        {
            UIManager.Instance.ShowAds();
            DataManager.Instance.AddCoin(50);
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
