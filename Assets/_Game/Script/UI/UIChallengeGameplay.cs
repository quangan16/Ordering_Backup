using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChallengeGameplay : MonoBehaviour,IUIControl
{

    [SerializeField] TextMeshProUGUI time;
    public TextMeshProUGUI tmp;
    [SerializeField] private List<Button> buttonsList;
    [SerializeField] private TextMeshProUGUI coinTxt;
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
        AdsAdapterAdmob.LogAFAndFB($"challenge_replay_level_" + GameManager.Instance.currentLevel, "0",
            "0");
        Debug.Log("challenge_replay_level_");
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
        this.coinTxt.text = coin.ToString();
    }
    //public void SetTime(float time)
    //{
    //    timer = time;
    //}

    public void DeactiveButtons()
    {
        foreach (var button in buttonsList)
        {
            button.interactable = false;
        }
    }

    public void ActiveButtons()
    {
        foreach (var button in buttonsList)
        {
            button.interactable = true;
        }
    }

    public TextMeshProUGUI GetCoinText()
    {
        return coinTxt;
    }

}
