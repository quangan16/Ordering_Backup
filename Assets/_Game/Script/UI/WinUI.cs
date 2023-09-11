using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [SerializeField] IndicatorController indi;

    [SerializeField] private Button adsButton;

    [SerializeField] private Button normalButton;
    // Start is called before the first frame update

    private void OnEnable()
    {
        adsButton.interactable = true;
        normalButton.interactable = true;
    }
    public void GetCoinDefault()
    {

        adsButton.interactable = false;
        normalButton.interactable = false;
        DataManager.Instance.AddCoin(int.Parse(indi.achievedCoinTxt.text));


    }
    public void GetCoinAds()
    {
        adsButton.interactable = false;
        normalButton.interactable = false;
        UIManager.Instance.ShowAds();
        DataManager.Instance.AddCoin(int.Parse(indi.adsCoinTxt.text));
    }
    public void Close()
    {
        gameObject.SetActive(false);
        GameManager.Instance.NextLevel();

    }
    public void Open()
    {
        gameObject.SetActive(true);
        // indi.Move();
        
    }

    


}
