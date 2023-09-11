using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonBehivour<DataManager>
{
    public void SetCoin(int coin)
    {
        PlayerPrefs.SetInt("coin", coin);
    }
    public int GetCoin(int coin)
    {
        return PlayerPrefs.GetInt("coin", 0);
    }
    public void SetTime(string time)
    {
        PlayerPrefs.SetString("time", time);
    }
    public string GetTime()
    {
        return PlayerPrefs.GetString("time","");
    }
    public void SetHeart(int heart)
    {
        PlayerPrefs.SetInt("heart", heart);
    }
    public int GetHeart()
    {
        return PlayerPrefs.GetInt("heart", 3);
    }
}
