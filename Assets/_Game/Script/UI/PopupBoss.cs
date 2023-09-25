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
        GameManager.Instance.StartCountDown();
    }
    public void PlayBtn()
    {
        OnClose();
        UIManager.Instance.OpenBoss();
    }

}
