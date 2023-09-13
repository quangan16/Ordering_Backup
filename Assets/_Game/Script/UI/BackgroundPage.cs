using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundPage : MonoBehaviour
{

    [SerializeField] private GameObject[] shopItems;

    public GameObject selectedObjectItem;
    private int selectedItemIndex = 0;

    [SerializeField] private Image background;

    private void OnEnable()
    {
        ShopItem.OnItemSelected += GetSelectItem;
        ShopItem.OnItemSelected += SelectItem;
    }

    private void OnDisable()
    {
        ShopItem.OnItemSelected -= SelectItem;
        ShopItem.OnItemSelected -= GetSelectItem;
    }

    public void GetSelectItem()
    {
        try
        {
            selectedItemIndex = System.Array.IndexOf(shopItems, selectedObjectItem);
            Debug.Log(selectedItemIndex);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public void DeselectItem()
    {
        shopItems[selectedItemIndex].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
    }

    public void SelectItem()
    {

        shopItems[selectedItemIndex].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        if (ShopGUI.currentPage == PageState.BACKGROUND)
        {
            background.color= shopItems[selectedItemIndex].transform.GetChild(0).GetChild(0).GetComponent<Image>()
                .color;
        }
    }
}
