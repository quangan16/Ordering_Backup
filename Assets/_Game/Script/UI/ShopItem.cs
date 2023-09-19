using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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
    [Serializable]
    public struct ShopItemData
    {
        public Sprite itemContent;
        public ItemType itemType;
    }



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
        price = skinItem.price;

    }
    public void OnInit(BackGroundType type)
    {
        ShopState state = DataManager.Instance.GetBackGroundState(type);
        BackGroundItem backGroundItem = DataManager.Instance.GetBackGround(type);
        SetState(state);
        price = backGroundItem.price;


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
          //  DataManager.Instance.
        }
    }
    public void Equip()
    {

    }
}
