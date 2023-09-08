using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBossGameplay : UIGamePlay,IUIControl
{
    [SerializeField] TextMeshProUGUI move;
    private void Start()
    {
        coin.text = PlayerPrefs.GetInt("coin", 0).ToString();
    }
    public void ShowMove(string move)
    {
        this.move.text= move;
    }

    

}
