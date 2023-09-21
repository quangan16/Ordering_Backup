using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAdsGUI : PopupManager
{
    public void Open()
    {
        gameObject.SetActive(true);
        OnOpen();

    }

    public void Close()
    {
        OnClose();
    }

    public void DenyBtn()
    {
       OnClose();
    }

    public void Accept()
    {
    }
}
