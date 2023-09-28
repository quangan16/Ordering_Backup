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
    [SerializeField] private Button playBtn;
    void OnEnable()
    {
        OnInit();
        // PlayButtonAnim();
    }

    private void OnDisable()
    {
        playBtn.transform.DOKill();
    }

    void OnInit()
    {
        coinTxt.text = DataManager.Instance.GetCoin().ToString();
        canvasAlpha.alpha = 1;
    }

    public void Open()
    {
        gameObject.SetActive(true);
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
        canvasAlpha.DOFade(0.0f, 0.6f).OnComplete(() => { gameObject.SetActive(false); });
        UIManager.Instance.OpenGameplay();
        
    }    
    public void OpenShop()
    {
        gameObject.SetActive(false);
        UIManager.Instance.OpenShop();  

    }

    public void OpenChallenge()
    {
        gameObject.SetActive(false);
        UIManager.Instance.OpenChallenge();
    }

    // public void PlayButtonReset()
    // {
    //     playBtn.transform.localEulerAngles = Vector3.zero;
    // }
    //
    // public void PlayButtonAnim()
    // {
    //     PlayButtonReset();
    //     playBtn.transform.
    //     playBtn.transform.DORotate(new Vector3(0, 0, 10.0f), 0.2f, RotateMode.Fast).SetEase(Ease.Linear)
    //         .SetLoops(20, LoopType.Yoyo).OnComplete(() => { playBtn.transform.localEulerAngles = Vector3.zero;
    //             PlayButtonAnim();
    //         });
    // }
    
}
