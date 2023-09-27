using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoAdsGUI : PopupManager
{
    public bool IsOpening = false;
    

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        if (IsOpening == false)
        {
            gameObject.SetActive(true);
            OnOpen();
            IsOpening = true;
        }

    }

   

   
}
