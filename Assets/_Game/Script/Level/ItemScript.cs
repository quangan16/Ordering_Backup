using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]

public class ItemScript :ScriptableObject
{
    public ItemData[] data;
    public ChallegeItemAnimation GetItem(Mode mode)
    {
        foreach( var i in data)
        {
            if (i.mode == mode)
            {
                return i.item;
            }
        }
        return null;
    }
}
[System.Serializable]
public class ItemData
{
    public Mode mode;

    public ChallegeItemAnimation item;
}

