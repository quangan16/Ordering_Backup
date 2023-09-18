using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    public bool ButtonClicked { get; private set; }
    [SerializeField] IndicatorController indi;

    [SerializeField] private Button adsButton;

    [SerializeField] private Button normalButton;

    public void Start()
    {
        
    }
   
    // Start is called before the first frame update

    private void OnEnable()
    {
        ButtonClicked = false;
        adsButton.interactable = true;
        normalButton.interactable = true;
        normalButton.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.Instance.current.rewards.ToString();
    }
    public void GetCoinDefault()
    {
        OnContinue();
        DataManager.Instance.AddCoin(int.Parse(indi.achievedCoinTxt.text));

    }
    public void GetCoinAds()
    {
        OnContinue();
        UIManager.Instance.ShowAds();
        DataManager.Instance.AddCoin(int.Parse(indi.adsCoinTxt.text));
    }

    public void PauseAnim()
    {
        if (ButtonClicked)
        {
           DOTween.PauseAll();
        }
    }

    public void OnContinue()
    {
        ButtonClicked = true;
        adsButton.interactable = false;
        normalButton.interactable = false;
        PauseAnim();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        OnNext();

    }
    public void Open()
    {
        gameObject.SetActive(true);

        
    }
    public void OnNext()
    {

        switch (GameManager.Instance.gameMode)
        {
            case GameMode.Normal:
                {
                    DataManager.Instance.SetNormalLevel(GameManager.Instance.currentLevel + 1);
                    
                    GameManager.Instance.OnWinNormal();
                    GameManager.Instance.NextLevel();
                    break;
                }
            case GameMode.Boss:
                {
                    
                    UIManager.Instance.OpenGameplay();
                    break;
                }
            case GameMode.Challenge:
                {
                    UIManager.Instance.OpenChallenge();
                    break;
                }

        }
    }




}
