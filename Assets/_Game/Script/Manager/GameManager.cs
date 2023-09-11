using System;
using UnityEngine;
public class GameManager : SingletonBehivour<GameManager>
{
    [SerializeField] LevelScript normal;
    public LevelScript challenge;
    [SerializeField] LevelScript boss;
    public GameMode gameMode;
    public Level current;
    public int currentLevel;
    public int normalLevel;






    public void OpenGamePlay(GameMode mode, int level)
    {
        switch (mode)
        {
            case GameMode.Normal:
                {
                    current = Instantiate(normal.levels[level]);

                    normalLevel = level;
                    break;
                }
            case GameMode.Challenge:
                {
                    UIChallengeGameplay.timer = current.time;
                    current = Instantiate(challenge.levels[level]);                   
                    break;
                }
            case GameMode.Boss:
                {
                    current = Instantiate(boss.levels[level]);
                    break;
                }
            default:
                {
                    break;
                }
        }
        gameMode= mode;
        currentLevel = level;
        int coin = DataManager.Instance.GetCoin();
        UIManager.Instance.SetCoin(coin);
        UIManager.Instance.SetText("Level " + (currentLevel+1));
        UIManager.Instance.SetTime(current.time);
        Camera.main.orthographicSize = current.cameraDist;

    }
    public void CloseGamePlay()
    {
        if (current != null)
        {
            Destroy(current);
            Destroy(current.gameObject);
        }
    }
    public void NextLevel()
    {
        CloseGamePlay();
        switch (gameMode)
        {   
            case GameMode.Normal:
                {
                    normalLevel++;
                    if (currentLevel >= normal.levels.Length)
                    {
                        currentLevel = 0;
                    }
                    Invoke(nameof(nextLevel), 0.1f);
                    break;
                }
                case GameMode.Challenge:
                {
                    UIManager.Instance.OpenChallenge();
                    break;
                }
                case GameMode.Boss:
                {
                    UIManager.Instance.OpenGameplay();
                    break;
                }
                default:
                {
                    break;
                }
        }
    }
    void nextLevel()
    {
        OpenGamePlay(GameMode.Normal, normalLevel);
    }
    public void Replay()
    {
        Solid.canClick = true;

        UIManager.Instance.ShowAds();
        CloseGamePlay();
        Invoke(nameof(ReStart), 0.1f);
    }
    void ReStart()
    {
        OpenGamePlay(gameMode, currentLevel);
    }





}
public enum GameMode
{
    Normal,
    Challenge,
    Boss
}
