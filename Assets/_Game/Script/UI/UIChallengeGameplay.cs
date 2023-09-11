using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIChallengeGameplay : MonoBehaviour,IUIControl
{

    [SerializeField] TextMeshProUGUI time;
    public TextMeshProUGUI tmp;
    public TextMeshProUGUI coin;
    public static float timer;
    public static bool isTouch = false;   
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

            if (Mathf.Abs(timer) < 0.01f)
            {
                timer = 0;
                UIManager.Instance.OpenLose();
                isTouch = false;
            }

            
        }
        DisplayTime(timer);

    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void Open()
    {
        isTouch = false;
        gameObject.SetActive(true);
    }
    public void Close()
    {
        isTouch = false;
        gameObject.SetActive(false);
    }
    public void Replay()
    {
        isTouch = false;
        GameManager.Instance.Replay();
    }
    public void OpenChallenge()
    {
        UIManager.Instance.OpenChallenge();

    }
    public void SetText(string text)
    {
        tmp.text = text;
    }
    public void SetCoin(int coin)
    {
        this.coin.text = coin.ToString();
    }
    public void SetTime(float time)
    {
        timer = time;
    }

}
