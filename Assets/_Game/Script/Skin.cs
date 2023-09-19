using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skin", order = 1)]
public class Skin : ScriptableObject
{
    public SkinItem[] skins;
    public SkinItem GetSkinItem(SkinType type)
    {
        foreach(var item in skins)
        {
            if(item.type == type)
            { return item; }
        }
        return null;
    }
}
[System.Serializable]
public class SkinItem
{
    public Sprite sprite;
    public int price;
    public SkinType type;
}
public enum SkinType
{
    strip,
    dot,

}