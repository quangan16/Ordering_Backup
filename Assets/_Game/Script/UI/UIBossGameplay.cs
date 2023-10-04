using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class UIBossGameplay : MonoBehaviour,IUIControl
{
    [SerializeField] Text time;
    [SerializeField] Text move;
    [SerializeField] Text coinTxt;
    [SerializeField] AudioSource audioSource;
    public Text level;
    [SerializeField] private List<Button> buttonsList;
    int seconds;
    


    private void Update()
    {
        DisplayTime(GameManager.timer);
        ShowMove(GameManager.moves);
    }
    public void ShowMove(int move)
    {
        this.move.text= move.ToString();
    }
    void DisplayTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        if(seconds != this.seconds)
        {
            this.seconds = seconds;
            audioSource.Play();
        }
        time.text = string.Format(Constant.TIME_FORMAT, minutes, seconds);
    }
    public void Open()
    {
        gameObject.SetActive(true);
        Invoke(nameof(OpenGamePlay), 0.1f);
    }
    public void OpenGamePlay()
    {
        GameManager.Instance.OpenGamePlay(GameMode.Boss, DataManager.Instance.GetBossLevel());

    }
    public void CloseGamePlay()
    {
        UIManager.Instance.OpenGameplay();
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void Replay()
    {
        AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
            {

                AdsAdapterAdmob.LogAFAndFB($"boss_replay_ads_level_" + GameManager.Instance.currentLevel +1, "0",
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
            AdsAdapterAdmob.where.boss_replay_ads_level);
       
    }
    public void SetText(string text)
    {
        level.text = text;
    }
    public void SetCoin(int coin)
    {
        this.coinTxt.text = coin.ToString();
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

    public Text GetCoinText()
    {
        return coinTxt;
    }


}
