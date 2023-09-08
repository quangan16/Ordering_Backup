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
        
    }
    public void ShowAds()
    {

    }
    public void NextLevel()
    {
        if(current is UIChallengeGameplay)
        {
            current.Close();
            OpenChallenge();
        }
        else if(current is UIGamePlay)
        {
            control.NextLevel();
        }
    
    }
    public void ShowWin()
    {
        win.Open();
    }
    public void OnWin()
    {
        control.OnWin();
    }
    public void OpenUI(IUIControl control)
    {
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
        challengeGameplay.InitLevel(level);

    }
    public void PlayEffect()
    {
        effect.PlayEffect();
    }



}