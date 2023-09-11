using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonBehivour<DataManager>
{
    public void SetCoin(int coin)
    {
        PlayerPrefs.SetInt("coin", coin);
    }
    public int GetCoin()
    {
        return PlayerPrefs.GetInt("coin", 0);
    }
    public void AddCoin(int coin)
    {
        SetCoin(GetCoin() + coin);
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
    public Mode GetLevelMode(int level)
    {
        string name = "level" + level;
        int t = PlayerPrefs.GetInt(name, 0);


        return (Mode)t;
    }
    public void SetLevel(int level,Mode mode)
    {
        string name = "level" + level;
        PlayerPrefs.SetInt(name, (int)mode);
    }
}
