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
    [SerializeField] private Button hintBtn;
    [SerializeField] private Image hintPriceHolder;
    [SerializeField] private Image hintAdsIcon;
    [SerializeField] private Image skipAdsIcon;

    private Tween buttonTween;
    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {
        buttonTween.Kill();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopHintAnim();
            GameManager.Instance.StartCountDown();
        }
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
        AdsAdapterAdmob.LogAFAndFB($"normal_replay_level_" + GameManager.Instance.currentLevel, "0",
            "0");
        Debug.Log("normal_replay_level_");
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
            AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
                {

                    AdsAdapterAdmob.LogAFAndFB($"normal_skip_level_", "0",
                        "0");
                    
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
                    Debug.Log("Failed to load");

                }, 0,
                AdsAdapterAdmob.where.normal_skip_level);
        }

        GameManager.Instance.NextLevel();
    }
    
    public virtual void CallHint()
    {
        //getHint = true;
        GameManager.Instance.DiscardRandom();
        GameManager.Instance.CheckFree();
           
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
        AdsAdapterAdmob.LogAFAndFB($"back_to_main_level_" + GameManager.Instance.currentLevel, "0",
            "0");
        if (DataManager.Instance.GetNormalLevel() >= 6)
        {
            AdsAdapterAdmob.Instance.ShowInterstitial(0, AdsAdapterAdmob.where.normal_back_to_main_level_);
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

    public void ShowHintCoinIcon()
    {
        hintPriceHolder.gameObject.SetActive(true);
    }

    public void HideHintCoinIcon()
    {
        hintPriceHolder.gameObject.SetActive(false);
    }

    public void HideHintAdIcon()
    {
        hintAdsIcon.gameObject.SetActive(false);
    }

    public void ShowHintAdIcon()
    {
        hintAdsIcon.gameObject.SetActive(true);
    }

    public void PlayHintAnim()
    {
       buttonTween = hintBtn.transform.DOScale(1.2f, 0.6f).SetLoops(-1, LoopType.Yoyo);
    }

    public void StopHintAnim()
    {
        if (buttonTween != null && buttonTween.IsPlaying())
        {
            buttonTween.Pause();
            hintBtn.transform.localScale = Vector3.one;
        }
       
    }

    public void ShowSkipAdsIcon()
    {
        skipAdsIcon.gameObject.SetActive(true);
    }

}
