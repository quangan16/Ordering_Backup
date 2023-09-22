using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class NoInternetGUI : PopupManager
{
    public bool IsOpening = false;
    new void  OnEnable()
    {
        GameManager.OnInternetError += Open;
        GameManager.OnInternetSuccess += Close;
    }

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

    public void Close()
    {
        
            
            gameObject.SetActive(false);
            OnClose();
        
    }

    public void Retry()
    {
        if (IsOpening)
        {
            IsOpening = false;
            OnClose();
        }
        
    }

    public new void OnClose()
    {
        mainPanel.DOScale(0.0f, scaleDuration).SetEase(Ease.InBack);
    }
    
    
    
    
}
