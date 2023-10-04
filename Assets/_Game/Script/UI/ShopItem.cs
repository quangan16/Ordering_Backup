using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] Image backGround;
    [SerializeField] Image skin;
    [SerializeField] Image ring;
    [SerializeField] TextMeshProUGUI priceText;
    //[SerializeField] GameObject Locked;
    [SerializeField] GameObject Unlocked;
    [SerializeField] Button selectBtn;
    [SerializeField] Button buyBtn;
    ItemType type;
    [SerializeField] int price;
    int onSelect;
    public ShopState state;




    //----------------------new-----------------------
    public void OnInit(SkinType type)
    {
        ShopState state = DataManager.Instance.GetRingSkinState(type);
        this.state = state;
        SkinItem skinItem = DataManager.Instance.GetSkin(type);
        // change ring skin
        price = skinItem.price;
        //SetState(state);
        backGround.gameObject.SetActive(false);
        skin.sprite = skinItem.spriteC;
        skin.color = skinItem.ChangeColorbyID(skinItem.spriteRing.name);
        ring.sprite = skinItem.spriteRing;
       
        onSelect = (int)type;
        this.type = ItemType.SKIN;

    }
    public void OnInit(BackGroundType type)
    {
        ShopState state = DataManager.Instance.GetBackGroundState(type);
        BackGroundItem backGroundItem = DataManager.Instance.GetBackGround(type);
        this.state = state;
        price = backGroundItem.price;
        //SetState(state);
        // change sprite background
        backGround.sprite = backGroundItem.buttonSprite;
        ring.gameObject.SetActive(false);
        

        onSelect = (int)type;
        this.type = ItemType.BACKGROUND;

    }
    //----------------x
    public void SetState(ShopState state)
    {
       // Locked.SetActive(false);
        switch (state)
        {
            case ShopState.Locked:
                {                   
                    break;
                }
            case ShopState.UnBought:
                {
                    // Locked.SetActive(true);
                    priceText.text = price.ToString();
                    break;
                }
            case ShopState.Bought:
                {
                    buyBtn.gameObject.SetActive(false);
                    break;
                }
            case ShopState.Equipped:
                {
                    buyBtn.gameObject.SetActive(false);
                    selectBtn.onClick.RemoveAllListeners();
                    selectBtn.onClick.AddListener(() => Equip());

                    borderSelect.SetActive(true);
                    break;
                }

        }
    }
    public void Buy()
    {
        if (DataManager.Instance.GetCoin() >= price)
        {
            
            DataManager.Instance.AddCoin(-price);
            Equip();
            UIManager.Instance.SetCoin();

            if (type == ItemType.SKIN)
            {
                AdsAdapterAdmob.LogAFAndFB($"ring_skin_bought_id_" + ((SkinType)onSelect), "0",
                    "0");
                Debug.Log((SkinType)onSelect);
            }
            else if (type == ItemType.BACKGROUND)
            {
                AdsAdapterAdmob.LogAFAndFB($"background_bought_id_" + ((BackGroundType)onSelect), "0",
                    "0");
                Debug.Log((BackGroundType)onSelect);
            }                       
        }
        else
        {
            UIManager.Instance.OpenNotEnough(NotEnoughType.Coin);
            UIManager.Instance.notEnough.AddListener(()=>Equip());

        }
    }
    public void Equip()
    {
        Debug.Log("ok bitch");
        
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
        SetState(ShopState.Equipped);
    }
    //------------------x
    public void AddEvent(UnityAction listener)
    {
        selectBtn.onClick.RemoveAllListeners();
        selectBtn.onClick.AddListener(listener);

        selectBtn.onClick.AddListener(() =>OnSelect());



    }
    public void OffSelect()
    {
        borderSelect.SetActive(false);

    }
    public void OnSelect()
    {
        borderSelect.SetActive(true);
    }    

}
