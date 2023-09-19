using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ItemType
{
    SKIN,
    BACKGROUND
}

public class ShopItem : MonoBehaviour
{



    [SerializeField] GameObject borderSelect;

    [SerializeField] Button selectBtn;
    [SerializeField] Button buyBtn;
    [SerializeField] Button equipBtn;
    ItemType type;
    int price;
    int onSelect;





    //----------------------new-----------------------
    public void OnInit(SkinType type)
    {
        ShopState state = DataManager.Instance.GetRingSkinState(type);
        SkinItem skinItem = DataManager.Instance.GetSkin(type);
        SetState(state);
        // change ring skin
        price = skinItem.price;
        
        onSelect = (int)type;
        this.type = ItemType.SKIN;

    }
    public void OnInit(BackGroundType type)
    {
        ShopState state = DataManager.Instance.GetBackGroundState(type);
        BackGroundItem backGroundItem = DataManager.Instance.GetBackGround(type);
        SetState(state);
        // change sprite background
        price = backGroundItem.price;
        onSelect = (int)type;
        this.type = ItemType.BACKGROUND;

    }
    void SetState(ShopState state)
    {
        switch (state)
        {
            case ShopState.Locked:
            {
                //active gameObject Locked
                break;
            }
            case ShopState.UnBought:
            {
                    // active gameObject UnBought
                    //priceText.text = price.ToString();
                    break;
            }
            case ShopState.Bought:
            {
                // active gameObject bought
                break;
            }
            case ShopState.Equipped:
            {
                    //change Button equip => equipped
                borderSelect.SetActive(true);
                break;
            }

        }
    }
    public void Buy()
    {
        if(DataManager.Instance.GetCoin()>=price)
        {
            DataManager.Instance.AddCoin(-price);
            Equip();
        }
        else
        {

        }
    }
    public void Equip()
    {
        if (type == ItemType.SKIN)
        {
            SkinType type = DataManager.Instance.GetLastRingSkin();
            DataManager.Instance.SetRingSkinState(ShopState.Bought, type);
            DataManager.Instance.SetRingSkinState(ShopState.Equipped, (SkinType)onSelect);
            DataManager.Instance.SetLastRingSkin((SkinType)onSelect);
        }
        else
        {
            BackGroundType type = DataManager.Instance.GetLastBackground();
            DataManager.Instance.SetBackGroundState(ShopState.Bought, type);
            DataManager.Instance.SetBackGroundState(ShopState.Equipped, (BackGroundType)onSelect);
            DataManager.Instance.SetLastBackground((BackGroundType)onSelect);   

        }
        //equipBtn => equipped 
        GetComponentInParent<ShopGUI>().ReLoad();
    }
    public void AddEvent(UnityAction listener)
    {
        selectBtn.onClick.AddListener(listener);
        
        selectBtn.onClick.AddListener(() => borderSelect.SetActive(true));

       // buyBtn.onClick.AddListener(listener);
       // equipBtn.onClick.AddListener(listener); 


    }
    public void OffSelect()
    {
        borderSelect.SetActive(false);
    }
}
