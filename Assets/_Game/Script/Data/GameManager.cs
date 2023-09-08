using System;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    public int  Coin { get; set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        SaveSystem.DeleteAllData();
        LoadSystem.LoadData();
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveData();
    }
    
    
}
