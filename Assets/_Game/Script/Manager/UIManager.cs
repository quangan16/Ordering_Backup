using UnityEngine;


public class UIManager : SingletonBehivour<UIManager>
{
    [SerializeField] ChallengeUI challenge;
    [SerializeField] WinUI win;
    [SerializeField] LoseUI lose;
    [SerializeField] UIBossGameplay boss;
    [SerializeField] UIChallengeGameplay challengeGameplay;
    [SerializeField] UIGamePlay control;
    [SerializeField] Effect effect;
    [SerializeField] PopupBoss popupBoss;
    [SerializeField] ChallengeUnlockPopup popupChallenge;

    IUIControl current;
    private void Start()
    {
        OpenGameplay();
        
    }
    public void ShowAds()
    {

    }

 
    public void OnWin()
    {
        win.Open();
        PlayEffect();
       
        
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
        print(DataManager.Instance.GetCoin());
    }
    //public void SetTime(float time)
    //{
    //    current.SetTime(time);  
    //}



}