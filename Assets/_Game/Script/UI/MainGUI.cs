using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainGUI : MonoBehaviour, IUIControl
{

    [SerializeField] private TextMeshProUGUI coinTxt;
    [SerializeField] private CanvasGroup canvasAlpha;
    [SerializeField] private List<Button> buttons;
    void OnEnable()
    {
        OnInit();
    }

    void OnInit()
    {
        coinTxt.text = DataManager.Instance.GetCoin().ToString();
        canvasAlpha.alpha = 1;
    }

    public void Open()
    {
        this.GameObject().SetActive(true);
        OnOpen();
    }

    public void Close()
    {
        OnClose();
    }

    public void OnClose()
    {
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
    }

    public void OnOpen()
    {
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
    }
    public void OpenGamePlay()
    {
        canvasAlpha.DOFade(0.0f, 1.0f).OnComplete(() => { gameObject.SetActive(false); });
      UIManager.Instance.OpenGameplay();
        
    }    
    public void OpenShop()
    {
        UIManager.Instance.OpenShop();  

    }

    public void OpenChallenge()
    {
        UIManager.Instance.OpenChallenge();
    }


    
}
