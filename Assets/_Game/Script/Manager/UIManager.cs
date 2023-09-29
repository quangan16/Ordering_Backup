using TMPro;
using UnityEngine;


public class UIManager : SingletonBehivour<UIManager>
{
    public ChallengeUI challenge;
    [SerializeField] WinUI win;
    [SerializeField] LoseUI lose;
    [SerializeField] UIBossGameplay boss;
    [SerializeField] UIChallengeGameplay challengeGameplay;
    [SerializeField] UIGamePlay control;
    [SerializeField] Effect effect;
    [SerializeField] PopupBoss popupBoss;
    [SerializeField] ChallengeUnlockPopup popupChallenge;
    [SerializeField] ShopGUI shop;
    [SerializeField] MainGUI mainMenu;
    [SerializeField] NotEnoughUI notEnough;
    [SerializeField] UIClamp uIClamp;

    [SerializeField] private NoInternetGUI noInternetGUI;
    [SerializeField] private RemoveAdsGUI removeAdsGUI;
    [SerializeField] private NoAdsGUI noAdsGUI;
    //[SerializeField] shop 
    public IUIControl PreviousScene { get; private set; }
    public IUIControl current;
    private void Start()
    {
       OpenMain();
    }
    public void ShowAds()
    {
       
    }

    public void ReloadShop()
    {
        (current as ShopGUI).ReLoad();
    }

    public void ReloadChallenge()
    {
        (current as ChallengeUI).OnInit();
    }

    public void DeactiveButtons()
    {
        current.DeactiveButtons();
    }

    public void ActiveButtons()
    {
        current.ActiveButtons();
    }
    
    public void OnWin()
    {
        
        
        if(GameManager.Instance.isWin )
        {
            if (GameManager.Instance.gameMode == GameMode.Normal)
            {
                AdsAdapterAdmob.LogAFAndFB($"normal_end_level_" + (GameManager.Instance.currentLevel + 1), "0",
                    "0");
                int normalLevel = DataManager.Instance.GetNormalLevel() + 1;
                if (normalLevel == 5)
                {
                    removeAdsGUI.Open();
                }
                if (normalLevel >= 5 && GameManager.Instance.levelLeftToShowAds<=0 ||
                    GameManager.Instance.timeLeftToShowAds <= 0.0f)
                {
                    AdsAdapterAdmob.Instance.ShowInterstitial(0, AdsAdapterAdmob.where.show_inter_end_level);
                    GameManager.Instance.levelLeftToShowAds = 2;
                }
            }
            else if (GameManager.Instance.gameMode == GameMode.Challenge)
            {
                AdsAdapterAdmob.LogAFAndFB($"challenge_end_level_" + (GameManager.Instance.currentLevel + 1), "0",
                    "0");
                if (GameManager.Instance.levelLeftToShowAds <= 0 || GameManager.Instance.timeLeftToShowAds <= 0.0f)
                {
                    AdsAdapterAdmob.Instance.ShowInterstitial(0, AdsAdapterAdmob.where.show_inter_end_level);
                    GameManager.Instance.levelLeftToShowAds = 2;
                }
                
            }
            else if (GameManager.Instance.gameMode == GameMode.Boss || GameManager.Instance.timeLeftToShowAds <= 0.0f)
            {
                AdsAdapterAdmob.LogAFAndFB($"boss_end_level_" + (GameManager.Instance.currentLevel + 1), "0",
                    "0");
                    if (GameManager.Instance.levelLeftToShowAds <= 0 || GameManager.Instance.timeLeftToShowAds <= 0.0f)
                    {
                        AdsAdapterAdmob.Instance.ShowInterstitial(0, AdsAdapterAdmob.where.show_inter_end_level);
                        GameManager.Instance.levelLeftToShowAds = 2;
                    }
            }
            PlayEffect();
            GameManager.Instance.OnWin();
            win.Open();

        }

       
        
    }
    public void OpenUI(IUIControl control)
    {
        GameManager.Instance.CloseGamePlay();
        GameManager.Instance.StopAllCoroutines();
        OffClamp();
        if(current!= null)
        {
            current.Close();           
        }
        
        PreviousScene = current;
        current = control;
        current.Open();
        
    }
    public void OpenChallenge()
    {
        OpenUI(challenge);
    }

    public void OpenChallengeAndScroll()
    {
        OpenUI(challenge);
        challenge.ScrollToNewestLevel();
    }
    public void OpenGameplay()
    {
        OpenUI(control);
     
        GameManager.Instance.ResetCountDown();

    }
    public void OpenBoss()
    {
        OpenUI(boss);
    }
    public void OpenLose(TypeOut type)
    {
        lose.Open(type);

    }
    public void RecommendChallenge()
    {
        popupChallenge.Open(); 
    }
    public void RecommendBoss()
    {
        popupBoss.Open();
       

    }
    public void OpenChallengeGameplay(int level)
    {
        OpenUI(challengeGameplay);
        GameManager.Instance.OpenGamePlay(GameMode.Challenge, level);

    }
    public void PlayEffect()
    {
        effect.PlayEffect();
    }
    public void SetText(string text)
    {
        current.SetText(text);
    }
    public void SetCoin()
    {
        current.SetCoin(DataManager.Instance.GetCoin());
    }
    public void OpenShop()
    {
        OpenUI(shop);
        SetCoin();

    }

    public void OpenMain()
    {
        OpenUI(mainMenu);
    }
    public void OpenNotEnough(NotEnoughType type)
    {
        notEnough.Open(type);
    }
    public TextMeshProUGUI CoinText()
    {
        return current.GetCoinText();
    }
    public void ShowClamp()
    {
        uIClamp.gameObject.SetActive(true);
        
    }    
    public void OnClampChange()
    {
        uIClamp.OnInit();
    }
    public void OffClamp()
    {
        uIClamp.gameObject.SetActive(false);
    }

    //public void SetTime(float time)
    //{
    //    current.SetTime(time);  
    //}

    public void ShowHintCoinIcon()
    {
        control.ShowHintCoinIcon();
    }

    public void HideHintCoinIcon()
    {
        control.HideHintCoinIcon();
    }

    public void ShowHintAdIcon()
    {
        control.ShowHintAdIcon();
    }

    public void HideHintAdIcon()
    {
        control.HideHintAdIcon();
    }

    public void StopHintAnim()
    {
        control.StopHintAnim();
    }
    public void PlayHintAnim()
    {
        control.PlayHintAnim();
    }

    public void ShowInternetPopUp()
    {
        noInternetGUI.Open();
    }

    public void ShowAdsNotification()
    {
        noAdsGUI.Open();
    }

    public void ShowSkipAdsIcon()
    {
        control.ShowSkipAdsIcon();
    }
}