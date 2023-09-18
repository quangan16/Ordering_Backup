using System;
using System.Collections;
using System.Collections.Generic;
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
   
}
