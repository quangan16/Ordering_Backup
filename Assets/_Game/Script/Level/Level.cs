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
    private void Awake()
    {
        OnInit();
    }
    private void OnDestroy()
    {
        
    }
    private void OnDisable()
    {
        
    }
    public void OnInit()
    {
        solidList = GetComponentsInChildren<Solid>().ToList();

    }
    public void DiscardRandom()
    {
        
        if(!isWin)
        {
            int remove = UnityEngine.Random.Range(0, solidList.Count);
            solidList[remove].OnDespawn();
            
        }
    }
    public void ChangeSkin()
    {
        SkinItem skinItem = DataManager.Instance.GetSkin(DataManager.Instance.GetLastRingSkin());
        Sprite spriteC = skinItem.spriteC;
        Sprite spriteL= skinItem.spriteL;
        foreach(var sol in solidList)
        {
            if(sol is Line)
            {
                sol.ChangeSkin(spriteL);

            }
            else
            {
                sol.ChangeSkin(spriteC);

            }
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
