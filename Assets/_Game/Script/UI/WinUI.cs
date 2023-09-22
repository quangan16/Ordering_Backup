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

    [SerializeField] private GameObject CoinRewardDisplay;

    [SerializeField] private Button denyBtn;

    public void Start()
    {
        
    }
   
    // Start is called before the first frame update

    private void OnEnable()
    {
        ButtonClicked = false;
        adsButton.interactable = true;
        denyBtn.interactable = false;
        CoinRewardDisplay.GetComponentInChildren<TextMeshProUGUI>().text ="+" +GameManager.Instance.current.rewards.ToString();
        ShowDenyButton();
    }
    public void GetCoinDefault()
    {
        OnContinue();
        DataManager.Instance.AddCoin(int.Parse(indi.defaultCoinTxt.text));

    }
    public void GetCoinAds()
    {
        OnContinue();
        UIManager.Instance.ShowAds();
        DataManager.Instance.AddCoin(int.Parse(indi.adsCoinTxt.text));
    }

    public void ShowDenyButton()
    {
        StartCoroutine(FadeIn());
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
        denyBtn.interactable = false;
        PauseAnim();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        UIManager.Instance.ActiveButtons();
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

    public IEnumerator FadeIn()
    {
        float duration = 1f; 
        float currentTime = 0f;
        var textColor = denyBtn.GetComponent<TextMeshProUGUI>();
        textColor.color = new Color(textColor.color.r, textColor.color.g, textColor.color.b, 0.0f);
        yield return new WaitForSeconds(2.0f);
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float alpha = Mathf.Lerp(0.0f,  1.0f, currentTime / duration);
                textColor.color = new Color(textColor.color.r, textColor.color.g, textColor.color.b, alpha);
                yield return null;
            }

            denyBtn.interactable = true;



    }




}
