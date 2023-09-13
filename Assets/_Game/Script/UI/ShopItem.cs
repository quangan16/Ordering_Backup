using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerClickHandler
{
    public bool hasBought;
    [SerializeField] private SkinPage skinPage;
    [SerializeField] private BackgroundPage backgroundPage;
    public static event Action OnItemSelected;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
