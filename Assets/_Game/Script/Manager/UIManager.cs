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
        current = control;
        GameManager.Instance.OpenGamePlay(GameMode.Normal, 0);
    }
    public void ShowAds()
    {

    }

    public void ShowWin()
    {
        win.Open();
    }
    public void OnWin()
    {
        win.Open();
    }
    public void OpenUI(IUIControl control)
    {
        GameManager.Instance.CloseGamePlay();
        current.Close();
        current= control;
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
    public void OpenLose()
    {
        lose.Open();
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
    public void SetTime(float time)
    {
        current.SetTime(time);  
    }



}