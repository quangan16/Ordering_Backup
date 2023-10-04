using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Solid> solidList ;
    public bool isWin => solidList.Count == 0;
    public float cameraDist = 8;
    public int moves;
    public float time;
    public int price;
    public int rewards;
    public bool isHint ;
    public bool firstHint;
    public string nameLevel = "Level ";
    private void Awake()
    {
        OnInit();
    }
    private void Start()
    {
        firstHint = DataManager.Instance.GetHint() == 0;
        isHint = true;
        if(firstHint)
        {
            UIManager.Instance.HideHintCoinIcon();
            UIManager.Instance.HideHintAdIcon();
        }

        else if (!firstHint && isHint)
        {
            UIManager.Instance.ShowHintCoinIcon();
            UIManager.Instance.HideHintAdIcon();
        }

        if (GameManager.Instance.currentLevel >= 4)
        {
            UIManager.Instance.ShowSkipAdsIcon();
        }

        if (UIManager.Instance.current is UIGamePlay)
        {
            AdsAdapterAdmob.LogAFAndFB($"normal_start_level_" + (GameManager.Instance.currentLevel + 1), "0",
                "0");
        }
        else if (UIManager.Instance.current is UIChallengeGameplay)
        {
            AdsAdapterAdmob.LogAFAndFB($"challenge_start_level_" + (GameManager.Instance.currentLevel + 1), "0",
                "0");
        }

        else if (UIManager.Instance.current is UIBossGameplay)
        {
            AdsAdapterAdmob.LogAFAndFB($"boss_start_level_" + (GameManager.Instance.currentLevel + 1), "0",
                "0");
        }
    }
    private void OnDestroy()
    {
        
    }
    private void OnDisable()
    {
        
    }
    public void OnInit()
    {
        isHint = true;
        solidList = GetComponentsInChildren<Solid>().ToList();
        GameManager.Instance.timeLeftToShowAds = 60.0f;
        GameManager.Instance.levelLeftToShowAds--;

    }
    
    

    public void Update()
    {
        if (!isWin)
        {
            GameManager.Instance.timeLeftToShowAds -= Time.deltaTime;
        }
       
        // Debug.Log(GameManager.Instance.timeLeftToShowAds);
    }
    public void CheckFree()
    {
        foreach(Solid solid in GetComponentsInChildren<Solid>()) 
        { 
            solid.CheckFree();
        }
    }    
    public void DiscardRandom()
    {
       
        if(!isWin)
        {
            firstHint = DataManager.Instance.GetHint() == 0;
            AdsAdapterAdmob.LogAFAndFB($"normal_button_hint_level_" + GameManager.Instance.currentLevel , "0",
                "0");
            if (!firstHint)
            {
               
                if (isHint)
                {
                    if(DataManager.Instance.GetCoin()>=50)
                    {
                        
                        DataManager.Instance.AddCoin(-50);
                        UIManager.Instance.SetCoin();
                        isHint = false;
                        UIManager.Instance.HideHintCoinIcon();
                        UIManager.Instance.ShowHintAdIcon();
                        Discard();
                    }    
                    else
                    {
                        UIManager.Instance.OpenNotEnough(NotEnoughType.Coin);
                    }    
                   
                }
                
                else
                {
                  
                    AdsAdapterAdmob.Instance.ShowRewardedVideo(() =>
                        {

                            AdsAdapterAdmob.LogAFAndFB($"get_hint_ads", "0",
                                "0");
                            Discard();
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
                        AdsAdapterAdmob.where.get_hint_ads);
                }
            }
            else
            {
                UIManager.Instance.ShowHintCoinIcon();
               Discard();
                DataManager.Instance.SetHint();
            }
           
           
            
            
        }
    }  
    void Discard()
    {
        
        Solid sol = null;
        while(sol == null)
        {
            int remove = UnityEngine.Random.Range(0, solidList.Count);
            Solid temp = solidList[remove];
            if((temp is Line)||(temp is Circle))
            {
                sol = temp;
            }
        }    
        sol.OnDespawnHint();
    }    
    public void ChangeSkin()
    {
        SkinItem skinItem = DataManager.Instance.GetSkin(DataManager.Instance.GetLastRingSkin());

        foreach(var sol in solidList)
        {
            sol.ChangeSkin(skinItem);
        }
    }
    
    public void CharacterFail()
    {
        Character[] characters = GetComponentsInChildren<Character>();
        foreach(Character character in characters)
        {
            character.Cry();
        }
    }    
    public void CharacterIdle()
    {
        Character[] characters = GetComponentsInChildren<Character>();
        foreach (Character character in characters)
        {
            character.InCage();
        }
    }


}
public class HintSorter : IComparer<Hint>
{
    public int Compare(Hint c1, Hint c2)
    {
        return c1.order.CompareTo(c2.order);
    }
}
[System.Serializable]
public class Hint
{
    public List<Solid> solidList = new List<Solid>();
    public int order;
    public Vector3[] Hints()
    {
        
        return solidList.Select(c=>c.transform.position).ToArray(); 

    }


}
