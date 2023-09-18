using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    [SerializeField] Image OutOfTime;
    [SerializeField] Image OutOfMove;
    [SerializeField] TextMeshProUGUI AddWhat;
    Type type;
    public void AddMoves()
    {
        UIManager.Instance.ShowAds();
        if(type == Type.TimeOut)
        {
            GameManager.timer = 15;

        }
        else
        {
            GameManager.moves = 5;
        }
        Close();
    }
    public void Deny()
    {
        if(GameManager.Instance.gameMode == GameMode.Challenge)
        {
            UIManager.Instance.OpenChallenge();

        }
        else
        {
            UIManager.Instance.OpenGameplay();
        }
        Close();
    }
    public void Close()
    {
        gameObject.SetActive(false);
        

    }
    public void Open(Type type)
    {
        this.type = type;
        if(type == Type.TimeOut)
        {
            OutOfTime.gameObject.SetActive(true);
            OutOfMove.gameObject.SetActive(false);
            AddWhat.text = "+15 SECONDS";
        }
        else
        {
            OutOfTime.gameObject.SetActive(false);
            OutOfMove.gameObject.SetActive(true);
            AddWhat.text = "+5 MOVES";

        }
        gameObject.SetActive(true);
    }
}
public enum Type
{
    MoveOut,
    TimeOut
}
