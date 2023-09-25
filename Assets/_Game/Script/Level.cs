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
    private void Awake()
    {
        OnInit();
    }
    private void Start()
    {
        firstHint = DataManager.Instance.GetHint() == 0;
       
        if (!firstHint  && isHint )
        {
            UIManager.Instance.ShowHintCoinIcon();
            UIManager.Instance.HideHintAdIcon();
        }

        else
        {
            UIManager.Instance.HideHintCoinIcon();
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

                            AdsAdapterAdmob.LogAFAndFB($"get_hint", "0",
                                "0");
                            Discard();
                        }, () =>
                        {
                            // PanelLoading.Instance.Notify("Watch Failed, Try Again!");
                            Debug.Log("Failed to load");

                        }, 0,
                        AdsAdapterAdmob.where.get_hint);
                }
            }
            else
            {
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
        Sprite spriteC = skinItem.spriteC;
        Sprite spriteL= skinItem.spriteL;
        foreach(var sol in solidList)
        {
            if(sol is Circle)
            {
                sol.ChangeSkin(spriteC);

            }
            else if(sol is Line || !(sol.CompareTag("Lock")))
            {
                sol.ChangeSkin(spriteL);

            }
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
