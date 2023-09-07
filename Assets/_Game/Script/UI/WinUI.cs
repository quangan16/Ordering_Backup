using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void GetCoinDefault()
    {

        Close();
    }
    public void GetCoinAds()
    {
        UIManager.Instance.ShowAds();
        Close();
    }
    public void Close()
    {
        gameObject.SetActive(false);
       UIManager.Instance.NextLevel();

    }
    public void Open()
    { gameObject.SetActive(true); }

    


}
