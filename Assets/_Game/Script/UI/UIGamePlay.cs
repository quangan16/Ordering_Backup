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
    [SerializeField] private TextMeshProUGUI hintPriceTxt;
    [SerializeField] private Image hintAdsIcon;

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
                    
                }, () =>
                {
                    // PanelLoading.Instance.Notify("Watch Failed, Try Again!");
                    Debug.Log("Failed to load");

                }, 0,
                AdsAdapterAdmob.where.skip_level);
        }

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

    public void ShowHintCoinIcon()
    {
        hintPriceTxt.gameObject.SetActive(true);
    }

    public void HideHintCoinIcon()
    {
        hintPriceTxt.gameObject.SetActive(false);
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

}
