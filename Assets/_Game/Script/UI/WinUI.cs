using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    public bool ButtonClicked { get; private set; }
    [SerializeField] IndicatorController indi;

    [SerializeField] private Button adsButton;

    [SerializeField] private GameObject CoinRewardDisplay;

    [SerializeField] private Button denyBtn;

    [SerializeField] private Text coinTxt;

    [SerializeField] ShopItem itemPref;
    
    [FormerlySerializedAs("skinLayout")] [SerializeField] Transform scrollLayout;

    int idSelect;
    int price;
    ItemType type;
    public void Start()
    {
        
    }

    public void SetCoinAfterBuy()
    {
        int targetCoin = DataManager.Instance.GetCoin();
        StartCoroutine(MinusCoinAnim(targetCoin));
    }

    private IEnumerator MinusCoinAnim(int coin)
    {
        while (coin < int.Parse(coinTxt.text))
        {
            coinTxt.text = (int.Parse(coinTxt.text) - 50).ToString();
            yield return null;
        }
    }
   
    // Start is called before the first frame update

    private void OnEnable()
    {
        ButtonClicked = false;
        adsButton.interactable = true;
        denyBtn.interactable = false;
        CoinRewardDisplay.GetComponentInChildren<Text>().text ="+" +GameManager.Instance.current.rewards.ToString();
        ShowDenyButton();
    }

    public void OnInit()
    {
        ReLoad();
    }
    public void GetCoinDefault()
    {
        OnContinue();
        DataManager.Instance.AddCoin(int.Parse(indi.defaultCoinTxt.text));

    }
    public void GetCoinAds()
    {
        OnContinue();
        if (GameManager.Instance.gameMode == GameMode.Normal)
        {
            AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
                {

                    AdsAdapterAdmob.LogAFAndFB($"normal_get_reward_ad_level_" + (GameManager.Instance.currentLevel + 1), "0",
                        "0");
                    DataManager.Instance.AddCoin(int.Parse(indi.adsCoinTxt.text));
                }, () =>
                {
                    StartCoroutine(GameManager.Instance.CheckInternetConnection());
                    if (GameManager.Instance.HasInternet == false)
                    {
                        UIManager.Instance.ShowInternetPopUp();
                    }
                    else
                    {
                        UIManager.Instance.ShowAdsNotification();
                    }
                    // Debug.Log("Failed to load");

                }, 0,
                AdsAdapterAdmob.where.normal_get_reward_ad);
        }

        else if (GameManager.Instance.gameMode == GameMode.Challenge)
        {
            AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
                {

                    AdsAdapterAdmob.LogAFAndFB($"challenge_get_reward_ad_level_" + (GameManager.Instance.currentLevel + 1), "0",
                        "0");
                    DataManager.Instance.AddCoin(int.Parse(indi.adsCoinTxt.text));
                }, () =>
                {
                    StartCoroutine(GameManager.Instance.CheckInternetConnection());
                    if (GameManager.Instance.HasInternet == false)
                    {
                        UIManager.Instance.ShowInternetPopUp();
                    }
                    else
                    {
                        UIManager.Instance.ShowAdsNotification();
                    }
                    Debug.Log("Failed to load");

                }, 0,
                AdsAdapterAdmob.where.challenge_get_reward_ad);
        }

        else if (GameManager.Instance.gameMode == GameMode.Boss)
        {
            AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
                {

                    AdsAdapterAdmob.LogAFAndFB($"boss_get_reward_ad_level_" + (GameManager.Instance.currentLevel + 1), "0",
                        "0");
                    DataManager.Instance.AddCoin(int.Parse(indi.adsCoinTxt.text));
                }, () =>
                {
                    StartCoroutine(GameManager.Instance.CheckInternetConnection());
                    if (GameManager.Instance.HasInternet == false)
                    {
                        UIManager.Instance.ShowInternetPopUp();
                    }
                    else
                    {
                        UIManager.Instance.ShowAdsNotification();
                    }
                    Debug.Log("Failed to load");

                }, 0,
                AdsAdapterAdmob.where.boss_get_reward_ad);
        }
    }

    public void ShowDenyButton()
    {
        StartCoroutine(FadeIn());
    }

    public void PauseAnim()
    {
        if (ButtonClicked)
        {
           DOTween.PauseAll();
        }
    }

    public void OnContinue()
    {
        ButtonClicked = true;
        adsButton.interactable = false;
        denyBtn.interactable = false;
        PauseAnim();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        UIManager.Instance.ActiveButtons();
        OnNext();

    }
    public void Open()
    {
        
        gameObject.SetActive(true);
        OnInit();

    }
    public void OnNext()
    {
        AdsAdapterAdmob.Instance.ShowBanner();
        switch (GameManager.Instance.gameMode)
        {
            case GameMode.Normal:
                {
                    DataManager.Instance.SetNormalLevel(GameManager.Instance.currentLevel + 1);
                    
                    GameManager.Instance.OnWinNormal();
                    GameManager.Instance.NextLevel();
                    break;
                }
            case GameMode.Boss:
                {
                    
                    UIManager.Instance.OpenGameplay();
                    break;
                }
            case GameMode.Challenge:
                {

                    UIManager.Instance.OpenChallenge();
                    break;
                }

        }
    }

    public IEnumerator FadeIn()
    {
        float duration = 0.7f; 
        float currentTime = 0f;
        var textColor = denyBtn.GetComponent<Text>();
        textColor.color = new Color(textColor.color.r, textColor.color.g, textColor.color.b, 0.0f);
        yield return new WaitForSeconds(1.5f);
            while (currentTime <= duration)
            {
              
                float alpha = Mathf.Lerp(0.0f,  1f, currentTime / duration);
                textColor.color = new Color(textColor.color.r, textColor.color.g, textColor.color.b, alpha);
                currentTime += Time.deltaTime;
                yield return null;
            }

            denyBtn.interactable = true;



    }
    
    public void ReLoad()
    {
        // SkinType skinType;
        // BackGroundType bgType;
        // BackGroundItem backGroundItem = DataManager.Instance.GetBackGround(bgType);
        // backgroundImg.sprite =backGroundItem.sprite;
        // if(type == ItemType.SKIN)
        // {
        //     ChangeButtonState(DataManager.Instance.GetRingSkinState(skinType));
        //
        // }
        // else
        // {
        //     ChangeButtonState(DataManager.Instance.GetBackGroundState(bgType));
        //
        // }
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
            if (DataManager.Instance.GetRingSkinState((SkinType)i) == ShopState.UnBought)
            {
                int ii = i;
                Debug.Log(ii);
                ShopItem shopItem = Instantiate(itemPref, scrollLayout);
                shopItem.transform.Find("DefaultShopItem/PriceBtn").gameObject.SetActive(true);
                shopItem.OnInit((SkinType)(ii));
                shopItem.AddEvent(() => shopItem.Buy());
                if (shopItem.state == ShopState.Equipped)
                {
                    shopItem.OnSelect();
                }    
            }
        
        }
        for (int j = 0; j < backGround.BackGroundItems.Length; j++)
        {
            if (DataManager.Instance.GetBackGroundState((BackGroundType)j) == ShopState.UnBought)
            {
                int jj = j;
                ShopItem shopItem = Instantiate(itemPref, scrollLayout);
                shopItem.transform.Find("DefaultShopItem/PriceBtn").gameObject.SetActive(true);
                shopItem.OnInit((BackGroundType)jj);
                shopItem.AddEvent(() => shopItem.Buy());

                if (shopItem.state == ShopState.Equipped)
                {
                    shopItem.OnSelect();
                }
            }                              
        }
    }
    
    




}
