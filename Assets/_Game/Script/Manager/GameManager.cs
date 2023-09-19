using System;
using System.Collections;
using UnityEngine;
public class GameManager : SingletonBehivour<GameManager>
{
    [SerializeField] LevelScript normal;
    public LevelScript challenge;
    [SerializeField] LevelScript boss;
    [SerializeField] TutoMove tutorial;
    [SerializeField] Transform tutoPos;
    public GameObject sprite;
    public GameMode gameMode;
    public Level current;
    public int currentLevel;
    public bool isWin => current.isWin;
    public static float timer;
    public static bool isTouch = false;
    public static int moves;
    public float adsCountdown;
    public static bool isVibrate = true;

    private void Start()
    {
        AdsAdapter.Instance.ShowBanner();
       // throw new NotImplementedException();
    }

    private void Update()
    {
        if (isTouch && (gameMode == GameMode.Challenge || gameMode == GameMode.Boss))
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0;
            }
            if (Mathf.Abs(timer) < 0.01f && !isWin)
            {
                timer = 0;
                UIManager.Instance.OpenLose(TypeOut.TimeOut);
                isTouch = false;
            }
        }
        else if (gameMode == GameMode.Normal)
        {
            adsCountdown += Time.deltaTime;
        }
    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(10);
        // Instantiate(tutorial, tutoPos);

    }
    public void StartCountDown()
    {
        StopAllCoroutines();
        StartCoroutine(CountDown());
    }
    public void OpenGamePlay(GameMode mode, int level)
    {
        isTouch = false;
        switch (mode)
        {
            case GameMode.Normal:
                {
                    if (level >= normal.levels.Length)
                    {
                        level = 0;
                    }


                    //Open challenge each 3 levels 2,5,8,...

                    if ((level - 1) % 3 == 0 && (level - 1) / 3 <= challenge.levels.Length - 1)
                    {
                        int levelChallenge = (level - 1) / 3;
                        (Mode challengeMode, int time) = DataManager.Instance.GetLevelMode(levelChallenge);
                        if (challengeMode == Mode.Locked)
                        {
                            DataManager.Instance.SetLevel(levelChallenge, Mode.Unlocked, 0);
                            UIManager.Instance.RecommendChallenge();
                        }

                    }
                    //Open boss each 6 levels 4,10,16,...

                    current = Instantiate(normal.levels[level]);
                    StartCountDown();
                    break;
                }
            case GameMode.Challenge:
                {
                    current = Instantiate(challenge.levels[level]);
                    break;
                }
            case GameMode.Boss:
                {
                    if (level >= boss.levels.Length)
                    {
                        level = 0;
                    }
                    current = Instantiate(boss.levels[level]);
                    break;
                }
            default:
                {
                    break;
                }
        }
        gameMode = mode;
        currentLevel = level;
        current.ChangeSkin();
        timer = (current.time);
        moves = current.moves;
        UIManager.Instance.SetCoin();
        UIManager.Instance.SetText("Level " + (currentLevel + 1));
        Camera.main.orthographicSize = current.cameraDist;

    }

    public void SubtractMove()
    {
        if (gameMode == GameMode.Boss)
        {
            moves--;

            if (moves <= 0)
            {
                isTouch = false;
                Invoke(nameof(OpenLoseMove), 0.1f);

            }
        }
    }
    void OpenLoseMove()
    {
        if (!isWin)
        {
            UIManager.Instance.OpenLose(TypeOut.MoveOut); // 
        }
    }

    public void CloseGamePlay()
    {
        isTouch = false;
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
        //Debug out range
        if (normalLevel >= normal.levels.Length)
        {
            normalLevel = 0;
        }
        if ((normalLevel - 3) % 6 == 0 && (normalLevel - 3) / 6 <= boss.levels.Length - 1)
        {

            UIManager.Instance.RecommendBoss();
            DataManager.Instance.SetBossLevel((normalLevel - 3) / 6);
        }
        currentLevel = normalLevel;
        Invoke(nameof(OnNextLevel), 0.1f);
    }
    void OnNextLevel()
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
        StopAllCoroutines();
        isTouch = false;
        if (gameMode == GameMode.Challenge)
        {
            (Mode mode, int time) = DataManager.Instance.GetLevelMode(currentLevel);
            if (mode != Mode.Pass)
            {
                int i = DataManager.Instance.GetTotalChallenge();
                i++;
                DataManager.Instance.SetTotalChallenge(i);
            }
            DataManager.Instance.SetLevel(currentLevel, Mode.Pass, Mathf.RoundToInt(timer) < time ? time : Mathf.RoundToInt(timer));
        }

    }
    public void OnWinNormal()
    {
        if (adsCountdown >= 60)
        {
            ResetCountDown();
            UIManager.Instance.ShowAds();
        }
    }
    public void ResetCountDown()
    {
        adsCountdown = 0;
    }
    public void DiscardRandom()
    {
        current.DiscardRandom();
    }





}
public enum GameMode
{
    Normal,
    Challenge,
    Boss
}
