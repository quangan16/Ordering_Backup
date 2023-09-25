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
        if(GameManager.Instance.isWin)
        {
            int normalLevel = DataManager.Instance.GetNormalLevel();
            if (normalLevel >= 5 && normalLevel % 3 == 0)
            {
                AdsAdapterAdmob.LogAFAndFB($"next_level", "0",
                    "0");
                AdsAdapterAdmob.Instance.ShowInterstitial(0, AdsAdapterAdmob.where.next_level);
            }

            win.Open();
            PlayEffect();
            GameManager.Instance.OnWin();
        }

       
        
    }
    public void OpenUI(IUIControl control)
    {
        GameManager.Instance.CloseGamePlay();
        GameManager.Instance.StopAllCoroutines();
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
}