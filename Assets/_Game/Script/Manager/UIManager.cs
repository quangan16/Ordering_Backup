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
    //[SerializeField] shop 

    IUIControl current;
    private void Start()
    {
       OpenMain();
        
    }
    public void ShowAds()
    {

    }
    public void OnWin()
    {
        if(GameManager.Instance.isWin)
        {
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
        current = control;
        current.Open();

    }
    public void OpenChallenge()
    {
        OpenUI(challenge);
    }
    public void OpenGameplay()
    {
        OpenUI(control);

    }
    public void OpenBoss()
    {
        OpenUI(boss);
    }
    public void OpenLose(Type type)
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
    }

    public void OpenMain()
    {
        OpenUI(mainMenu);
    }
    public void OpenNotEnough(NotEnoughType type)
    {
        notEnough.Open(type);
    }



    //public void SetTime(float time)
    //{
    //    current.SetTime(time);  
    //}



}