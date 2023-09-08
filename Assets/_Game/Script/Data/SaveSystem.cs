using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
   
    // Start is called before the first frame update
    public static void SaveData()
    {
        PlayerPrefs.SetInt("Coin", GameManager.Instance.Coin);
        PlayerPrefs.Save();
    }

    public static void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
    }
}
