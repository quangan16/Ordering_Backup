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
    private void Update()
    {   
        DisplayTime(GameManager.timer);

    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void Replay()
    {
        GameManager.Instance.Replay();
    }
    public void OpenChallenge()
    {
        UIManager.Instance.OpenChallenge();

    }
    public void SetText(string text)
    {
        tmp.text = "Challenge "+ text;
    }
    public void SetCoin(int coin)
    {
        this.coin.text = coin.ToString();
    }
    //public void SetTime(float time)
    //{
    //    timer = time;
    //}

}
