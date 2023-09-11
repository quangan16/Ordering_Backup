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
       

    }
    public void GetCoinAds()
    {
        UIManager.Instance.ShowAds();
        
    }
    public void Close()
    {
        gameObject.SetActive(false);
        GameManager.Instance.NextLevel();

    }
    public void Open()
    {
        gameObject.SetActive(true);
        // indi.Move();
        
    }

    


}
