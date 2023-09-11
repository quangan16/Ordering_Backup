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
        UIManager.Instance.SetCoin(0);
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
                    OpenGamePlay(GameMode.Normal,normalLevel);
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
                    current = Instantiate(normal.levels[normalLevel]);
                    break;
                }
                default:
                {
                    break;
                }
        }
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
