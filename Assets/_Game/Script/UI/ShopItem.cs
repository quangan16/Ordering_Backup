using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerClickHandler
{
    public bool hasBought;
    
    public static event Action OnItemSelected;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (hasBought == true)
        {
            ShopGUI.Instance.DeselectItem();
            ShopGUI.Instance.selectedObjectItem = gameObject;
            OnItemSelected?.Invoke();
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
