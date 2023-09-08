using UnityEngine;


public class UIManager : SingletonBehivour<UIManager>
{
    [SerializeField] UIControl control;
    [SerializeField] WinUI win;
    [SerializeField] ChallengeUI challenge;
    public void ShowAds()
    {

    }
    public void NextLevel()
    {
        control.NextLevel();
    }
    public void ShowWin()
    {
        win.Open();
    }
    public void OnWin()
    {
        control.OnWin();
    }


}