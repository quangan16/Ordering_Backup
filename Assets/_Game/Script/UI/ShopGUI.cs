using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;



public class ShopGUI : MonoBehaviour, IUIControl
{
    [SerializeField] private Image skinButtonImg;

    [SerializeField] private Image backgroundButtonImg;

    // Start is called before the first frame update
    [SerializeField] private GameObject skinPage;
    [SerializeField] private GameObject backgroundPage;

    [SerializeField] private Color buttonActiveColor;
    [SerializeField] private Color buttonInactiveColor;
    [SerializeField] private Color textActiveColor;
    [SerializeField] private Color textInactiveColor;

    [SerializeField] private TextMeshProUGUI skinTxt;
    [SerializeField] private TextMeshProUGUI backgroundTxt;
    [SerializeField] TextMeshProUGUI priceTxt;

    [SerializeField] private Image ringSkinLeft;
    [SerializeField] private Image ringSkinRight;
    [SerializeField] private Image backgroundImg;
    [SerializeField] private Image LightSFX;

    [SerializeField] ShopItem itemPref;
    [SerializeField] Transform skinLayout;
    [SerializeField] Transform backgroundLayout;

    [SerializeField] private TextMeshProUGUI coinTxt;
    [SerializeField] GameObject buttonBuy;
    [SerializeField] GameObject buttonAd;



    static int idSelect;
    int price;
    static ItemType type;
    void OnEnable()
    {
        //backgroundImg.sprite = shopItemData.shopItems[DataManager.Instance.GetBackground()].itemContent;
        LightEffect();
        
    }
    public void SetCoin(int coin)
    {
        coinTxt.text = coin.ToString();
    }    
    void OnDisable()
    {
        DOTween.KillAll();
    }




    void Start()
    {
        type = ItemType.SKIN;
        skinPage.gameObject.SetActive(true);
        backgroundPage.gameObject.SetActive(false);
    }


    public void OnSkinPageSelect()
    {
        skinButtonImg.color = buttonActiveColor;
        backgroundButtonImg.gameObject.GetComponentInChildren<Image>().color = buttonInactiveColor;
        skinPage.gameObject.SetActive(true);
        backgroundPage.gameObject.SetActive(false);
        skinTxt.color = textActiveColor;
        backgroundTxt.color = textInactiveColor;
    }

    public void OnBackgroundPageSelect()
    {
        skinButtonImg.gameObject.GetComponentInChildren<Image>().color = buttonInactiveColor;
        backgroundButtonImg.gameObject.GetComponentInChildren<Image>().color = buttonActiveColor;
        skinPage.gameObject.SetActive(false);
        backgroundPage.gameObject.SetActive(true);
        skinTxt.color = textInactiveColor;
        backgroundTxt.color = textActiveColor;
    }

    public void ResetLightEffect()
    {
        LightSFX.transform.localScale = Vector3.one;
        LightSFX.transform.localEulerAngles = Vector3.zero;
    }

    public void LightEffect()
    {
        ResetLightEffect();
        LightSFX.transform.DOScale(Vector3.one * 0.5f, 0.8f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        LightSFX.transform.DORotate(Vector3.forward * 360.0f, 4.0f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        OnInit();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void BackBtn()
    {
        if (UIManager.Instance.PreviousScene is MainGUI)
        {
            UIManager.Instance.OpenMain();
        }
        else if (UIManager.Instance.PreviousScene is UIGamePlay)
        {
            UIManager.Instance.OpenGameplay(); 
        }
       
    }
    //--------------------new----------------------
    public void OnInit()
    {
       
        ReLoad();
    }
    public void ReLoad()
    {
        SkinType skinType = DataManager.Instance.GetLastRingSkin();
        ringSkinLeft.sprite = DataManager.Instance.GetSkin(skinType).spriteC;
        ringSkinRight.sprite = DataManager.Instance.GetSkin(skinType).spriteC;
        if (skinType == SkinType.Strip || skinType == SkinType.LeoPattern)
        {
            ringSkinRight.color = new Color32(117, 15, 15, 80);
            ringSkinLeft.color = new Color32(121, 27, 20, 80);
        }
        else
        {
            ringSkinRight.color = new Color32(249, 200, 222, 200);
            ringSkinLeft.color = new Color32(227, 117, 0, 200);

        }
        BackGroundType bgType = DataManager.Instance.GetLastBackground();
        BackGroundItem backGroundItem = DataManager.Instance.GetBackGround(bgType);
        backgroundImg.sprite =backGroundItem.sprite;
        if(type == ItemType.SKIN)
        {
            ChangeButtonState(DataManager.Instance.GetRingSkinState(skinType));

        }
        else
        {
            ChangeButtonState(DataManager.Instance.GetBackGroundState(bgType));

        }
        Skin skins = DataManager.Instance.skins;
        N_BackGround backGround = DataManager.Instance.backGround;
        ShopItem[] items = GetComponentsInChildren<ShopItem>(true);
        foreach (ShopItem item in items)
        {
            Destroy(item.gameObject);
            Destroy(item);
        }

        for (int i = 0; i < skins.skins.Length; i++)
        {
            int ii = i;
            ShopItem shopItem = Instantiate(itemPref, skinLayout);
            shopItem.OnInit((SkinType)(ii));
            shopItem.AddEvent(() => SelectSkin((SkinType)(ii)));
            if(shopItem.state == ShopState.Equipped)
            {
                shopItem.OnSelect();
            }    
        }
        for (int j = 0; j < backGround.BackGroundItems.Length; j++)
        {
            int jj = j;
            ShopItem shopItem = Instantiate(itemPref, backgroundLayout);
            shopItem.OnInit((BackGroundType)jj);
            shopItem.AddEvent(() => SelectBackGround((BackGroundType)jj));
            if (shopItem.state == ShopState.Equipped)
            {
                shopItem.OnSelect();
            }
        }
    }
    public void SelectSkin(SkinType skinType)
    {
        ShopItem[] items = GetComponentsInChildren<ShopItem>();
        foreach (ShopItem item in items)
        {
           // if(item.state != ShopState.Equipped)
            item.OffSelect();
        }
        SkinItem itemSkin = DataManager.Instance.GetSkin(skinType);
        ringSkinLeft.sprite = itemSkin.spriteC;
        ringSkinRight.sprite = itemSkin.spriteC;
        if (skinType == SkinType.Strip || skinType == SkinType.LeoPattern)
        {
            ringSkinRight.color = new Color32(117, 15, 15, 80);
            ringSkinLeft.color = new Color32(121, 27, 20, 80);
        }
        else
        {
            ringSkinRight.color = new Color32(249, 200, 222, 200);
            ringSkinLeft.color = new Color32(227, 117, 0, 200);

        }

        priceTxt.text = itemSkin.price.ToString();
        price = itemSkin.price;
        type = ItemType.SKIN;
        idSelect = (int)skinType;
        ChangeButtonState(DataManager.Instance.GetRingSkinState(skinType));
    }
    public void SelectBackGround(BackGroundType backGroundType)
    {
        ShopItem[] items = GetComponentsInChildren<ShopItem>(true);
        foreach (ShopItem item in items)
        {
           // if (item.state != ShopState.Equipped)
                item.OffSelect();
        }
        BackGroundItem backGroundItem = DataManager.Instance.GetBackGround(backGroundType);
        backgroundImg.sprite = backGroundItem.sprite;
        priceTxt.text = backGroundItem.price.ToString();
        price = backGroundItem.price;
        type = ItemType.BACKGROUND;
        idSelect = (int)backGroundType;
        ChangeButtonState(DataManager.Instance.GetBackGroundState(backGroundType));

    }
    public void Buy()
    {
        if(DataManager.Instance.GetCoin()>=price)
        {
            DataManager.Instance.AddCoin(-price);
            UIManager.Instance.SetCoin();
            Equip();
            ChangeButtonState(ShopState.Equipped);
        }
        else
        {
            UIManager.Instance.OpenNotEnough(NotEnoughType.Coin);
        }

    }    
    public static void Equip()
    {
        if (type == ItemType.SKIN)
        {
            SkinType type = DataManager.Instance.GetLastRingSkin();
            DataManager.Instance.SetRingSkinState(ShopState.Bought, type);
            DataManager.Instance.SetRingSkinState(ShopState.Equipped, (SkinType)idSelect);
            DataManager.Instance.SetLastRingSkin((SkinType)idSelect);
        }
        else
        {
            BackGroundType type = DataManager.Instance.GetLastBackground();
            DataManager.Instance.SetBackGroundState(ShopState.Bought, type);
            DataManager.Instance.SetBackGroundState(ShopState.Equipped, (BackGroundType)idSelect);
            DataManager.Instance.SetLastBackground((BackGroundType)idSelect);

        }
        //ChangeButtonState(ShopState.Equipped);

    }
    void ChangeButtonState(ShopState state)
    {
        buttonBuy.SetActive(false);
        buttonAd.SetActive(false);
        switch (state)
        {
            case ShopState.Bought:
                {
                    Equip();
                    ChangeButtonState(ShopState.Equipped);
                    break;
                }
            case ShopState.Equipped:
                {
                   
                    break;
                }
            case ShopState.UnBought:
                {
                    buttonBuy.SetActive(true);
                    buttonAd.SetActive(true);
                    break;
                }

        }


    }

}