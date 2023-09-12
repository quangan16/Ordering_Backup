using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class UIBossGameplay : MonoBehaviour,IUIControl
{
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI move;
    public TextMeshProUGUI coin;
    public TextMeshProUGUI level;


    private void Update()
    {
        DisplayTime(GameManager.timer);
        ShowMove(GameManager.moves);
    }
    public void ShowMove(int move)
    {
        this.move.text= move.ToString();
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
        GameManager.Instance.OpenGamePlay(GameMode.Boss, DataManager.Instance.GetBossLevel());
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void Replay()
    {
        GameManager.Instance.Replay();
    }
    public void SetText(string text)
    {
        level.text = text;
    }
    public void SetCoin(int coin)
    {
        this.coin.text = coin.ToString();
    }
    public void Exit()
    {
        UIManager.Instance.OpenGameplay();
    }



}
