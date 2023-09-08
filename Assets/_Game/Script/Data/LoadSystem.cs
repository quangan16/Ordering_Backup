using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSystem : MonoBehaviour
{
    public static void LoadData()
    {
        GameManager.Instance.Coin =  PlayerPrefs.GetInt("Coin" , 0);
        
    }
    
}
