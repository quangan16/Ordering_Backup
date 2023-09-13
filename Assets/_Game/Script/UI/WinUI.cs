using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    public bool ButtonClicked { get; private set; }
    [SerializeField] IndicatorController indi;

    [SerializeField] private Button adsButton;

    [SerializeField] private Button normalButton;
   
    // Start is called before the first frame update

    private void OnEnable()
    {
        ButtonClicked = false;
        adsButton.interactable = true;
        normalButton.interactable = true;
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
            Debug.Log(DOTween.PauseAll());
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
        

    }
    public void Open()
    {
        gameObject.SetActive(true);
        // indi.Move();
        
    }

    


}
