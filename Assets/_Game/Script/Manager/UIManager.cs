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
        if(GameManager.Instance.gameMode == GameMode.Challenge)
        {
            GameManager.isTouch = false;
            int level = GameManager.Instance.currentLevel;
            (Mode mode, int time) = DataManager.Instance.GetLevelMode(level);
            DataManager.Instance.SetLevel(level, Mode.Pass, Mathf.RoundToInt(GameManager.timer) < time?time: Mathf.RoundToInt(GameManager.timer));
        }
    }
    public void OpenUI(IUIControl control)
    {
        GameManager.Instance.CloseGamePlay();
        if(current!= null)
        {
            current.Close();           
        }
        current = control;
        current.Open();

    }
    public void RePlay()
    {
        OpenUI(current);
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
    public void SetCoin(int coin)
    {
        current.SetCoin(coin);
    }
    //public void SetTime(float time)
    //{
    //    current.SetTime(time);  
    //}



}