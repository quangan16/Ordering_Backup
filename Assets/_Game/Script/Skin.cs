using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Sprite spriteC;
    public Sprite spriteL;
    public int price;
    public SkinType type;
    public Sprite spriteRing;
    public Color ChangeColorbyID(string s)
    {
        
        Color color = Color.white;
        byte alpha = 80;
        
        string numberString = new string(s.Where(char.IsDigit).ToArray());
        int i;
        if (!string.IsNullOrEmpty(numberString))
        {
            if (int.TryParse(numberString, out i) && i >= 1 && i <= 10)
            {
                if (type == SkinType.LeoPattern )
                {
                    switch (i)
                    {
                        case 1:
                            {
                                color = new Color32(95, 27, 18, alpha);
                                break;
                            }
                        case 2:
                            {
                                color = new Color32(121, 27, 20, alpha);
                                break;
                            }

                        case 3:
                            {
                                color = new Color32(117, 15, 15, alpha);
                                break;
                            }
                        case 4:
                            {
                                color = new Color32(87, 29, 116, alpha);
                                break;
                            }
                        case 5:
                            {
                                color = new Color32(90, 116, 44, alpha);
                                break;
                            }
                        case 6:
                            {
                                color = new Color32(113, 0, 0, alpha);
                                break;
                            }
                        case 7:
                            {
                                color = new Color32(130, 43, 43, alpha);
                                break;
                            }
                        case 8:
                            {
                                color = new Color32(140, 53, 68, alpha);
                                break;
                            }
                        case 9:
                            {
                                color = new Color32(57, 125, 35, alpha);
                                break;
                            }
                        case 10:
                            {
                                color = new Color32(99, 27, 71, alpha);
                                break;
                            }

                    }
                }
                else if (type == SkinType.Star || type == SkinType.Snow || type == SkinType.Wavy)
                {
                    alpha = 200;
                    switch (i)
                    {
                        case 1:
                            {
                                color = new Color32(69, 219, 233, alpha);
                                break;
                            }
                        case 2:
                            {
                                color = new Color32(227, 117, 0, alpha);
                                break;
                            }

                        case 3:
                            {
                                color = new Color32(249, 200, 222, alpha);
                                break;
                            }
                        case 4:
                            {
                                color = new Color32(254, 229, 255, alpha);
                                break;
                            }
                        case 5:
                            {
                                color = new Color32(192, 241, 197, alpha);
                                break;
                            }
                        case 6:
                            {
                                color = new Color32(246, 216, 139, alpha);
                                break;
                            }
                        case 7:
                            {
                                color = new Color32(252, 196, 246, alpha);
                                break;
                            }
                        case 8:
                            {
                                color = new Color32(196, 252, 247, alpha);
                                break;
                            }
                        case 9:
                            {
                                color = new Color32(252, 247, 196, alpha);
                                break;
                            }
                        case 10:
                            {
                                color = new Color32(252, 196, 246, alpha);
                                break;
                            }
                    }
                }
                else if(type == SkinType.Cloud)
                {
                    color.a = 0.9f;
                }    
            }


        }
        else
        {
            color.a = 0.5f;
            if (type == SkinType.Cloud)
            {
                color.a = 0.9f;
            }
        }
        return color;
    }
}
public enum SkinType
{
    Default,
    Star,
    Cloud,
    Wavy,
    LeoPattern,
    Snow
    

}