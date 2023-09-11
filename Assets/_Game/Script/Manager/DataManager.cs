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
    public (Mode,int) GetLevelMode(int level)
    {
        string name = "level" + level;
        string[] t = PlayerPrefs.GetString(name, "").Split(" ");


        return ((Mode)int.Parse(t[0]), int.Parse(t[1]));
    }
    public void SetLevel(int level,Mode mode, int time)
    {
        string name = "level" + level;
        string t = (int)mode + " " + time;
        PlayerPrefs.SetString(name, t);
    }
    public int GetNormalLevel()
    {
       return PlayerPrefs.GetInt("normal", 0);
    }
    public void SetNormalLevel(int level)
    {
        PlayerPrefs.SetInt("normal", level);
    }
}
