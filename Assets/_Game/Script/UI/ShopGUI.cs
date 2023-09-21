using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PageState
{
    SKIN,
    BACKGROUND
}

public class ShopGUI : MonoBehaviour, IUIControl
{
    public static ShopGUI Instance;

    public static PageState currentPage;


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


    [SerializeField] private Image ringSkin;
    [SerializeField] private Image backgroundImg;
    [SerializeField] private Image LightSFX;

    [SerializeField] ShopItem itemPref;
    [SerializeField] Transform skinLayout;
    [SerializeField] Transform backgroundLayout;

    [SerializeField] private TextMeshProUGUI coinTxt;

    void OnEnable()
    {
        //backgroundImg.sprite = shopItemData.shopItems[DataManager.Instance.GetBackground()].itemContent;
        LightEffect();
        coinTxt.text = DataManager.Instance.GetCoin().ToString();
    }

    void OnDisable()
    {
        DOTween.KillAll();
    }


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        skinPage.gameObject.SetActive(true);
        backgroundPage.gameObject.SetActive(false);
       // currentPage = PageState.SKIN;
    }


    public void OnSkinPageSelect()
    {
        skinButtonImg.color = buttonActiveColor;
        backgroundButtonImg.gameObject.GetComponentInChildren<Image>().color = buttonInactiveColor;
        skinPage.gameObject.SetActive(true);
        backgroundPage.gameObject.SetActive(false);
        skinTxt.color = textActiveColor;
        backgroundTxt.color = textInactiveColor;
        currentPage = PageState.SKIN;
    }

    public void OnBackgroundPageSelect()
    {
        skinButtonImg.gameObject.GetComponentInChildren<Image>().color = buttonInactiveColor;
        backgroundButtonImg.gameObject.GetComponentInChildren<Image>().color = buttonActiveColor;
        skinPage.gameObject.SetActive(false);
        backgroundPage.gameObject.SetActive(true);
        skinTxt.color = textInactiveColor;
        backgroundTxt.color = textActiveColor;
        currentPage = PageState.BACKGROUND;
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
        ringSkin.sprite = DataManager.Instance.GetSkin(DataManager.Instance.GetLastRingSkin()).spriteC;
        backgroundImg.sprite = DataManager.Instance.GetBackGround(DataManager.Instance.GetLastBackground()).sprite;
        ReLoad();
    }
    public void ReLoad()
    {
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
        }
        for (int j = 0; j < backGround.BackGroundItems.Length; j++)
        {
            int jj = j;
            ShopItem shopItem = Instantiate(itemPref, backgroundLayout);
            shopItem.OnInit((BackGroundType)jj);
            shopItem.AddEvent(() => SelectBackGround((BackGroundType)jj));
        }
    }
    public void SelectSkin(SkinType skinType)
    {
        ShopItem[] items = GetComponentsInChildren<ShopItem>();
        foreach (ShopItem item in items)
        {
            if(item.state != ShopState.Equipped)
            item.OffSelect();
        }
        
        ringSkin.sprite = DataManager.Instance.GetSkin(skinType).spriteC;
    }
    public void SelectBackGround(BackGroundType backGroundType)
    {
        ShopItem[] items = GetComponentsInChildren<ShopItem>(true);
        foreach (ShopItem item in items)
        {
            if (item.state != ShopState.Equipped)
                item.OffSelect();
        }
        backgroundImg.sprite = DataManager.Instance.GetBackGround(backGroundType).sprite;
    }











}