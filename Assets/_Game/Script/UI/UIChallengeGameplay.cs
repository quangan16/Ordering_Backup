using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIChallengeGameplay : UIGamePlay,IUIControl
{

    [SerializeField] TextMeshProUGUI time;
    public static float timer;
    public static bool isTouch = false;
    
    private void Start()
    {
        coin.text = PlayerPrefs.GetInt("coin", 0).ToString();
    }
    private void Update()
    {
        if (isTouch)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0;
            }


        }
        if (Mathf.Abs(timer)<0.01f )
        {
            UIManager.Instance.OpenLose();
            isTouch= false;
        }
        
        DisplayTime(timer);
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public override void Open()
    {
        enabled = true;
        isTouch = false;
        gameObject.SetActive(true);
    }
    public override void Close()
    {
        enabled = false;
        if (current)
        {
            current.gameObject.SetActive(false);
        }
        isTouch = false;
        gameObject.SetActive(false);
    }
    public void InitLevel(int level)
    {
        currentLevel= level;
        Load();
        timer = current.time;
    }

}
