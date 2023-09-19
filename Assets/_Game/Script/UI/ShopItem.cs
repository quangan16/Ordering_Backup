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

[Serializable]
public class ShopItem : MonoBehaviour, IPointerClickHandler
{
    public bool hasBought;
    [SerializeField] private SkinPage skinPage;
    [SerializeField] private BackgroundPage backgroundPage;
    public static event Action OnItemSelected;
    int price;
    int onSelect;
    [SerializeField] Button selectBtn;
    ItemType type;




    public void OnPointerClick(PointerEventData eventData)
    {
        switch (ShopGUI.currentPage)
        {
            case PageState.SKIN:
                if (hasBought == true)
                {
                    skinPage.DeselectItem();
                    skinPage.selectedObjectItem = gameObject;
                    OnItemSelected?.Invoke();
                }
                break;
            case PageState.BACKGROUND:
                if (hasBought == true)
                {
                    backgroundPage.DeselectItem();
                    backgroundPage.selectedObjectItem = gameObject;
                    OnItemSelected?.Invoke();
                }

                break;
        }



    }
    //----------------------new-----------------------
    public void OnInit(SkinType type)
    {
        ShopState state = DataManager.Instance.GetRingSkinState(type);
        SkinItem skinItem = DataManager.Instance.GetSkin(type);
        SetState(state);
        // change ring skin
        // price = skinItem.price;
        price = 0;
        onSelect = (int)type;
        this.type = ItemType.SKIN;

    }
    public void OnInit(BackGroundType type)
    {
        ShopState state = DataManager.Instance.GetBackGroundState(type);
        BackGroundItem backGroundItem = DataManager.Instance.GetBackGround(type);
        SetState(state);
        // change sprite background
        //price = backGroundItem.price;
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
                break;
            }

        }
    }
    public void Buy()
    {
        if(DataManager.Instance.GetCoin()>=price)
        {
            DataManager.Instance.AddCoin(-price);
            if(type == ItemType.SKIN)
            {
                DataManager.Instance.SetRingSkinState(ShopState.Equipped, (SkinType)onSelect);
            }
            else
            {
                DataManager.Instance.SetBackGroundState(ShopState.Equipped,(BackGroundType)onSelect);   
            }
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
    }
    public void AddEvent(UnityAction listener)
    {
        selectBtn.onClick.RemoveAllListeners();
        selectBtn.onClick.AddListener(listener);    
    }

}
