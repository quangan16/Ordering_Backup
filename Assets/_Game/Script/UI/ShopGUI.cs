using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public enum PageState
{
    SKIN,
    BACKGROUND
}

public class ShopGUI : MonoBehaviour
{
    public static ShopGUI Instance;

    public static PageState currentPage;
   
    

    [SerializeField] private Image skinButtonImg;

    [SerializeField] private Image backgroundButtonImg;
    // Start is called before the first frame update
    [SerializeField] private GameObject skinPage;
    [SerializeField] private GameObject backgroundPage;

    [SerializeField] private Color buttonActiveColor;
    [SerializeField] private Color buttonInactiveColor;
    [SerializeField] private Color textActiveColor;
    [SerializeField] private Color textInactiveColor;
    
    [SerializeField] private TextMeshProUGUI skinTxt;
    [SerializeField] private TextMeshProUGUI backgroundTxt;

    

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // private void OnEnable()
    // {
    //     ShopItem.OnItemSelected += GetSelectItem;
    //     ShopItem.OnItemSelected += SelectItem;
    // }

    // private void OnDisable()
    // {
    //     ShopItem.OnItemSelected -= SelectItem;
    //     ShopItem.OnItemSelected -= GetSelectItem;
    // }

    void Start()
    {
        skinPage.gameObject.SetActive(true);
        backgroundPage.gameObject.SetActive(false);
        currentPage = PageState.SKIN;
    }

    // public void GetSelectItem()
    // {
    //     try
    //     {
    //         selectedItemIndex = System.Array.IndexOf(shopItems, selectedObjectItem);
    //         Debug.Log(selectedItemIndex);
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         throw;
    //     }
    //   
    // }

    public void OnSkinPageSelect()
    {
        skinButtonImg.color = buttonActiveColor;
        backgroundButtonImg.gameObject.GetComponentInChildren<Image>().color = buttonInactiveColor;
        skinPage.gameObject.SetActive(true);
        backgroundPage.gameObject.SetActive(false);
        skinTxt.color = textActiveColor;
        backgroundTxt.color = textInactiveColor;
        currentPage = PageState.SKIN;
    }

    public void OnBackgroundPageSelect()
    {
        
        skinButtonImg.gameObject.GetComponentInChildren<Image>().color = buttonInactiveColor;
        backgroundButtonImg.gameObject.GetComponentInChildren<Image>().color = buttonActiveColor;
        skinPage.gameObject.SetActive(false);
        backgroundPage.gameObject.SetActive(true);
        skinTxt.color = textInactiveColor;
        backgroundTxt.color = textActiveColor;
        currentPage = PageState.BACKGROUND;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    // public void DeselectItem()
    // {
    //     shopItems[selectedItemIndex].transform.GetChild(0).GetChild(2).gameObject.SetActive(false); 
    // }
    //
    // public void SelectItem()
    // {
    //     
    //     shopItems[selectedItemIndex].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
    //     if (currentPage == PageState.SKIN)
    //     {
    //         ringSkin.sprite = shopItems[selectedItemIndex].transform.GetChild(0).GetChild(1).GetComponent<Image>()
    //             .sprite;
    //     }
    // }
}
