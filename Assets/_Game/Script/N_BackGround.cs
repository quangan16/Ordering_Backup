using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BackGround", order = 1)]

public class N_BackGround : ScriptableObject
{
    public BackGroundItem[] BackGroundItems;
    public BackGroundItem GetBackGround(BackGroundType type)
    {
        foreach(BackGroundItem item in BackGroundItems)
        {
            if(item.type == type)
                return item;
        }
        return null;
    }
}
[System.Serializable]
public class BackGroundItem
{
    public BackGroundType type;
    public int price;
    public Sprite sprite;
}
public enum BackGroundType
{
    Red,
    Yellow,
    Blue,
    Black
}
