using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseUI : MonoBehaviour
{
    public void AddMoves()
    {
        UIManager.Instance.ShowAds();
        UIManager.Instance.SetTime(15);
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
        

    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
}
