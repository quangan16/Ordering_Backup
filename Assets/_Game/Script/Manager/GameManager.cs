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

    public static float timer;
    public static bool isTouch = false;
    public static int moves;


    private void Update()
    {
        if (isTouch && (gameMode == GameMode.Challenge|| gameMode == GameMode.Boss))
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0;
            }
            if (Mathf.Abs(timer) < 0.01f)
            {
                timer = 0;
                UIManager.Instance.OpenLose(Type.time);
                isTouch = false;
            }
        }
    }
    public void OpenGamePlay(GameMode mode, int level)
    {
        isTouch = false;
        switch (mode)
        {
            case GameMode.Normal:
                {
                    current = Instantiate(normal.levels[level]);
                    break;
                }
            case GameMode.Challenge:
                {                    
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
        timer = (current.time);
        moves = current.moves;
        int coin = DataManager.Instance.GetCoin();
        UIManager.Instance.SetCoin(coin);
        UIManager.Instance.SetText("Level " + (currentLevel+1));
        Camera.main.orthographicSize = current.cameraDist;

    }
    public void SubtractMove()
    {
        if( gameMode == GameMode.Boss )        
        {
            moves--;
            if (moves == 0)
            {
                isTouch = false;
                UIManager.Instance.OpenLose(Type.move); // 
            }
        }
       
    }
    public void CloseGamePlay()
    {
        isTouch= false;
        if (current != null)
        {
            Destroy(current);
            Destroy(current.gameObject);
        }
    }
    public void NextLevel()
    {
        CloseGamePlay();
        int normalLevel = DataManager.Instance.GetNormalLevel();    
        if (normalLevel >= normal.levels.Length)
        {
            normalLevel = 0;
        }
        currentLevel= normalLevel;
        Invoke(nameof(nextLevel), 0.1f);
    }
    void nextLevel()
    {
        OpenGamePlay(GameMode.Normal, currentLevel);
    }
    public void Replay()
    {
        Solid.canClick = true;
        isTouch = false;
        UIManager.Instance.ShowAds();
        CloseGamePlay();
        Invoke(nameof(ReStart), 0.1f);
    }
    void ReStart()
    {
        OpenGamePlay(gameMode, currentLevel);
    }
    public void OnWin()
    {
        isTouch = false;
    }





}
public enum GameMode
{
    Normal,
    Challenge,
    Boss
}
