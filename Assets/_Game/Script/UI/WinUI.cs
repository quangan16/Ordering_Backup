using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    [SerializeField] IndicatorController indi;

    // Start is called before the first frame update
    public void GetCoinDefault()
    {
        int coin = PlayerPrefs.GetInt("coin", 0);
        coin += 12;
        PlayerPrefs.SetInt("coin",coin);
        Close();

    }
    public void GetCoinAds()
    {
        UIManager.Instance.ShowAds();
        GetCoinDefault();
        
    }
    public void Close()
    {
        gameObject.SetActive(false);
        enabled= false;
        UIManager.Instance.NextLevel();

    }
    public void Open()
    {
        gameObject.SetActive(true);
        enabled = true;
        // indi.Move();
        
    }

    


}
