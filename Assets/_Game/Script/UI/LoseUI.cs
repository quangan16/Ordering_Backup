using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseUI : MonoBehaviour
{
    public void AddMoves()
    {
        UIManager.Instance.ShowAds();
        UIChallengeGameplay.timer += 15;
        Close();
    }
    public void Deny()
    {
        UIManager.Instance.OpenChallenge();
        Close();
    }
    public void Close()
    {
        gameObject.SetActive(false);
        enabled = false;
        

    }
    public void Open()
    {
        gameObject.SetActive(true);
        enabled = true;
    }
}
