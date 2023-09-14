using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public TextMeshProUGUI coin;
    public int heart;
    DateTime dateTime;
    string format = "dd-MM-yyyy HH:mm:ss";
    private void FixedUpdate()
    {
        CheckHeart();
    }
    void OnInit()
    {
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
                        challenge.SetData(j * 3+2);
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
            dateTime = DateTime.Now;
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
        UIManager.Instance.OpenGameplay();
    }

    public void Open()
    {
        OnInit();
        coin.text = DataManager.Instance.GetCoin().ToString();
        heart = DataManager.Instance.GetHeart();
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

            
            UIManager.Instance.OpenChallengeGameplay(level);
        }

    }
    public void AddHeart()
    {
        UIManager.Instance.ShowAds();
        heart++;

    }
    private void OnApplicationQuit()
    {
        Close();
    }
    public void SetCoin(int coin)
    {
        this.coin.text = coin.ToString();
    }

}