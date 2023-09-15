using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class UIGamePlay : MonoBehaviour,IUIControl
{
    // Start is called before the first frame update
    public TextMeshProUGUI tmp;
    public TextMeshProUGUI coin;
    public static bool getHint = false;

    public virtual void Open()
    {
        gameObject.SetActive(true);
        GameManager.Instance.OpenGamePlay(GameMode.Normal, DataManager.Instance.GetNormalLevel());
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
        UIManager.Instance.ShowAds();
        GameManager.Instance.NextLevel();
    }

    public virtual void CallHint()
    {
        getHint = true;
           
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





}
