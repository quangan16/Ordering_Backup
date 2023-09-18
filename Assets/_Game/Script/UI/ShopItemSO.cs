using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "ShopItemData", menuName = "ScriptableObjects/ShopItemSO", order = 3)]
public class ShopItemSO : ScriptableObject
{
    public ShopItem.ShopItemData[] shopItems;
    // Start is called before the first frame update

}
