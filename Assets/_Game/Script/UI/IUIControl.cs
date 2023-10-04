using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IUIControl
{
    void Open()
    {

    }
    void Close() { }
    void SetText(string text) { } //Set levelText
    void SetCoin(int coin) { }
    //void SetTime(float time) { }
    void DeactiveButtons()
    {
        
    }

    void ActiveButtons()
    {
        
    }
    Text GetCoinText()
    {
        return null;
    }
         
}
