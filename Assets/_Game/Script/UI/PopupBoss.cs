using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBoss : PopupManager
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
    public void PlayBtn()
    {
        OnClose();
        UIManager.Instance.OpenBoss();
    }

}
