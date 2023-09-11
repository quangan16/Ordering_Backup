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
    [SerializeField] ChallegeItemAnimation challengeItem;
    [SerializeField] GameObject layout;
    public TextMeshProUGUI coin;
    public int heart;
    [SerializeField] TextMeshProUGUI heartText;
    [SerializeField] TextMeshProUGUI timeTxt;
    [SerializeField] Button add;
    DateTime dateTime;
    string format = "dd-MM-yyyy HH:mm:ss";
    private void Start()
    {
        levels = GameManager.Instance.challenge.levels.ToList();
        coin.text = PlayerPrefs.GetInt("coin", 0).ToString();
        for (int i = 0; i < levels.Count; i++)
        {
            int j = i;
            ChallegeItemAnimation challenge = Instantiate(challengeItem, layout.transform);
            challenge.playButton.onClick.AddListener( () => OpenLevel(j));
            challenge.SetData((i + 1).ToString());
        }
    }
    private void Update()
    {
        CheckHeart();
    }
    void SetTime(TimeSpan time)
    {
        float timeToDisplay = (float)(time.TotalSeconds);
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
            TimeSpan timespan = DateTime.Now - dateTime;
            TimeSpan time = new TimeSpan(0, 0, 15);
            while (time < timespan)
            {
                timespan -= time;
                heart++;
                if (heart == 3)
                {
                    return;

                }
            }
            time -= timespan;
            SetTime(time);
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
        heart = PlayerPrefs.GetInt("heart", 0);
        gameObject.SetActive(true);

        string s = PlayerPrefs.GetString("time", "");
        string[] day = s.Split(" ")[0].Split("-");
        string[] hour = s.Split(" ")[1].Split(":");
        dateTime = new DateTime(int.Parse(day[2]), int.Parse(day[1]), int.Parse(day[0]), int.Parse(hour[0]), int.Parse(hour[1]), int.Parse(hour[2]));
      
    }

    public void Close()
    {
        PlayerPrefs.SetInt("heart", heart);
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
                print(dateTime);
                PlayerPrefs.SetString("time", dateTime.ToString(format));
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


}