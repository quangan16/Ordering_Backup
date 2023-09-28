using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeUI : MonoBehaviour, IUIControl
{

    [SerializeField] List<Level> levels;
    [SerializeField] ItemScript challengeItem;
    [SerializeField] GameObject layout;
    [SerializeField] TextMeshProUGUI heartText;
    [SerializeField] TextMeshProUGUI timeTxt;
    [SerializeField] Button add;
    public int totalChallengePassed;
    public TextMeshProUGUI coin;
    public int heart;
    DateTime dateTime;
    string format = "dd-MM-yyyy HH:mm:ss";
    public static int levelID;

    [SerializeField] private Slider challengeBar;
    [SerializeField] private TextMeshProUGUI passedChallengeTxt;
    public ScrollRect scrollRect;
    public RectTransform content;
    public int unlockedItemIndex; // Index of the unlocked item you want to scroll to.
    public static ChallengeBought challengeBought;
    private void OnEnable()
    {
        UpdateChallengeBar();
        Invoke(nameof(ScrollToNewestLevel), 0.5f);
    }

    
    private float CalculateNormalizedYPosition()
    {
        Canvas.ForceUpdateCanvases();
        int challengeLevel = DataManager.Instance.GetMaxLevelUnlock();
        if (challengeLevel < 0 || challengeLevel >= content.childCount)
        {
            return 0f;
        }

      
        float itemHeight = content.GetChild(0).rect().rect.height + 60;
        float contentHeight = LayoutUtility.GetPreferredHeight(content);
        float yPosition = ((challengeLevel /3) * itemHeight) /(contentHeight - 3 * itemHeight);
        return Mathf.Clamp01(1 - yPosition);
    }
    [Button]
    public void ScrollToNewestLevel()
    {
        float targetPos = CalculateNormalizedYPosition();
        scrollRect.DOVerticalNormalizedPos(targetPos, 0.7f).SetEase(Ease.InOutSine);
    }
    
   
    private void FixedUpdate()
    {
        CheckHeart();
    }
    public void OnInit()
    {
        totalChallengePassed = DataManager.Instance.GetTotalChallenge();
        coin.text = DataManager.Instance.GetCoin().ToString();
        heart = DataManager.Instance.GetHeart();
        levels = GameManager.Instance.challenge.levels.ToList();
        ChallegeItemAnimation[] challegeItems = layout.transform.GetComponentsInChildren<ChallegeItemAnimation>();
        foreach(var challegeItem in challegeItems)
        {
            Destroy(challegeItem.gameObject);
            Destroy(challegeItem);
        }
        for (int i = 0; i < levels.Count; i++)
        {
            int j = i;
            (Mode mode, int time) = DataManager.Instance.GetLevelMode(j);
            ChallegeItemAnimation challenge = Instantiate(challengeItem.GetItem(mode), layout.transform);
            challenge.level = j;
            challenge.dataLevel = levels[i];
            switch (mode)
            {
                case Mode.Locked:
                    {
                        challenge.SetData(j * 4+2+4);
                        break;
                    }
                case Mode.Unlocked:
                    {
                        int price = levels[j].price;
                        challenge.SetData(price);
                        (challenge as ChallengeBought).SetChildren(() => OpenLevel(j));
                        break;
                    }
                case Mode.Bought:
                    {
                        challenge.playButton.onClick.AddListener(() => OpenLevel(j));
                        int rewards = levels[j].rewards;
                        challenge.SetData(rewards);
                        break;
                    }
                case Mode.Pass:
                    {
                        challenge.playButton.onClick.AddListener(() => OpenLevel(j));
                        challenge.SetData(time);
                        break;
                    }
                case Mode.Fail:
                    {
                        challenge.playButton.onClick.AddListener(() => OpenLevel(j));
                        int rewards = levels[j].rewards;
                        challenge.SetData(rewards);
                        break;
                    }
                default:
                    {
                        break;
                    }

            }

        }
    }
    void SetTime(TimeSpan time)
    {
        TimeSpan timeSpan = new TimeSpan(0,30, 0);
        while (timeSpan<time && heart <3)
        {
            time -= timeSpan;
            heart++;
            dateTime = DateTime.Now - time;
            DataManager.Instance.SetTime(dateTime.ToString(format));
            if (heart == 3)
            {
                return;
            }

        }
        
        timeSpan -= time;
        float timeToDisplay = (float)(timeSpan.TotalSeconds);
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);


    }
    void CheckHeart()
    {
        if(heart == 3)
        {
            timeTxt.text = "MAX";
            dateTime = DateTime.Now;
            add.gameObject.SetActive(false);
        }
        else
        {

            SetTime(DateTime.Now - dateTime);
            add.gameObject.SetActive(true);
        }
        heartText.text = heart.ToString();
    }

    public void BackBtn()
    {
        UIManager.Instance.OpenMain();
    }

    public void Open()
    {
        OnInit();
        // UpdateChallengeBar();
        GetComponent<CanvasGroup>().alpha = 1f;
        gameObject.SetActive(true);
        string s = DataManager.Instance.GetTime();
        string[] day = s.Split(" ")[0].Split("-");
        string[] hour = s.Split(" ")[1].Split(":");
        dateTime = new DateTime(int.Parse(day[2]), int.Parse(day[1]), int.Parse(day[0]), int.Parse(hour[0]), int.Parse(hour[1]), int.Parse(hour[2]));
   


    }

    public void Close()
    {
        DataManager.Instance.SetHeart(heart);
        gameObject.SetActive(false);
    }

    public void OpenLevel(int level)
    {
        if(heart>0)
        {
            heart--;
           if(heart == 2)
            {
                dateTime= DateTime.Now;
                DataManager.Instance.SetTime(dateTime.ToString(format));
            }
            (Mode mode, int t) = DataManager.Instance.GetLevelMode(level);
           if ( mode == Mode.Bought)
            {
                DataManager.Instance.SetLevel(level, Mode.Fail, 0);

            }
            UIManager.Instance.OpenChallengeGameplay(level);
        }
        else
        {
            UIManager.Instance.OpenNotEnough(NotEnoughType.Heart);
        }

    }
    public void AddHeart(int addAmount)
    {
        heart += addAmount;
    }

    public void OnHeartBtnClick(int addAmount)
    {
        AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
            {

                AdsAdapterAdmob.LogAFAndFB($"get_live", "0",
                    "0");
               AddHeart(3);
            }, () =>
            {
                
                Debug.Log("Failed to load");
                
            }, 0,
            AdsAdapterAdmob.where.get_live);
    }
    private void OnApplicationQuit()
    {
        Close();
    }
    public void SetCoin(int coin)
    {
        this.coin.text = coin.ToString();
    }

    public void UpdateChallengeBar()
    {
        challengeBar.value = totalChallengePassed;
        passedChallengeTxt.text = totalChallengePassed.ToString() + "/20";
    }


 


}