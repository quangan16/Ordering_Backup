using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChallengeGameplay : MonoBehaviour,IUIControl
{

    [SerializeField] Text time;
    public Text tmp;
    [SerializeField] private List<Button> buttonsList;
    [SerializeField] private Text coinTxt;
    private void Update()
    {   
        DisplayTime(GameManager.timer);

    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        time.text = string.Format(Constant.TIME_FORMAT, minutes, seconds);
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void Replay()
    {
       
        AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
            {

                AdsAdapterAdmob.LogAFAndFB($"challenge_replay_ads_level_" + (GameManager.Instance.currentLevel + 1), "0",
                    "0");
                GameManager.Instance.Replay();
               
            }, () =>
            {
                StartCoroutine(GameManager.Instance.CheckInternetConnection());
                if (GameManager.Instance.HasInternet == false)
                {
                    UIManager.Instance.ShowInternetPopUp();
                }
                else
                {
                    UIManager.Instance.ShowAdsNotification();
                }

            }, 0,
            AdsAdapterAdmob.where.challenge_replay_ads_level);
       
    }
    public void OpenChallenge()
    {
        UIManager.Instance.OpenChallenge();

    }
    public void SetText(string text)
    {
        tmp.text = text;
    
    }
    public void SetCoin(int coin)
    {
        this.coinTxt.text = coin.ToString();
    }
    //public void SetTime(float time)
    //{
    //    timer = time;
    //}

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

    public Text GetCoinText()
    {
        return coinTxt;
    }

}
