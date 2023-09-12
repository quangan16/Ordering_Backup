using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopGUI : MonoBehaviour
{
    [SerializeField] private ButtonEffectLogic skinBtn;

    [SerializeField] private ButtonEffectLogic backgroundBtn;
    // Start is called before the first frame update
    [SerializeField] private GameObject skinPage;
    [SerializeField] private GameObject pageGroup;

    [SerializeField] private Color buttonActiveColor;
    [SerializeField] private Color buttonInactiveColor;

    [SerializeField] private Color pageActiveColor;
    
    
    

    public void OnSkinPageSelect()
    {
        skinBtn.gameObject.GetComponentInChildren<Image>().color = buttonActiveColor;
        backgroundBtn.gameObject.GetComponentInChildren<Image>().color = buttonInactiveColor;
    }

    public void OnBackgroundSelect()
    {
        skinBtn.gameObject.GetComponentInChildren<Image>().color = buttonInactiveColor;
        backgroundBtn.gameObject.GetComponentInChildren<Image>().color = buttonActiveColor;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnActive()
    {
        
    }
}
