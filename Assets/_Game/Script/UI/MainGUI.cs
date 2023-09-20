using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MainGUI : MonoBehaviour, IUIControl
{

    [SerializeField] private TextMeshProUGUI coinTxt;
    void OnEnable()
    {
        coinTxt.text = DataManager.Instance.GetCoin().ToString();
    }

    public void Open()
    {
        this.GameObject().SetActive(true);
    }

    public void Close()
    {
        this.GameObject().SetActive(false);
    }
    public void OpenGamePlay()
    {
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
