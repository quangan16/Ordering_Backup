using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinPage : MonoBehaviour
{

    [SerializeField] private GameObject[] shopItems;

    public GameObject selectedObjectItem;
    private int selectedItemIndex = 0;

    [SerializeField] private Image ringSkin;

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
        if (ShopGUI.currentPage == PageState.SKIN)
        {
            ringSkin.sprite = shopItems[selectedItemIndex].transform.GetChild(0).GetChild(1).GetComponent<Image>()
                .sprite;
        }
    }
}
