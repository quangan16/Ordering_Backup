using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonBehivour<DataManager>
{
    public Skin skins;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
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
        return PlayerPrefs.GetString("time",DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
    }

    public void SetHeart(int heart)
    {
        PlayerPrefs.SetInt("heart", heart);
    }
    public int GetHeart()
    {
        return PlayerPrefs.GetInt("heart", 3);
    }

    public void SetTotalChallenge(int totalChallenge)
    {
        PlayerPrefs.SetInt("totalchallenge", totalChallenge);
    }    
    public int GetTotalChallenge()
    {
        return PlayerPrefs.GetInt("totalchallenge", 0);
    }

    public (Mode,int) GetLevelMode(int level)
    {
        string name = "level" + level;
        string[] t = PlayerPrefs.GetString(name, "0 0").Split(" ");


        return ((Mode)int.Parse(t[0]), int.Parse(t[1]));
    }
    public void SetLevel(int level,Mode mode, int time)
    {
        string name = "level" + level;
        string t = (int)mode + " " + time;
        PlayerPrefs.SetString(name, t);
    } //challlenge

    public int GetNormalLevel()
    {
       return PlayerPrefs.GetInt("normal", 0);
    }
    public void SetNormalLevel(int level)
    {
        PlayerPrefs.SetInt("normal", level);
    }

    public int GetBossLevel()
    {
        return PlayerPrefs.GetInt("boss", 0);
    }
    public void SetBossLevel(int level)
    {
        PlayerPrefs.SetInt("boss", level);
    }

    public void SetLastRingSkin(SkinType type) //last
    {
        PlayerPrefs.SetInt("LastSkin", (int)type);
    }

    public SkinType GetLastRingSkin() //last
    {
        return (SkinType)PlayerPrefs.GetInt("LastSkin");
    }
    public void SetRingSkinState(ShopState state,SkinType type)
    {
        PlayerPrefs.SetInt("skin" + type.ToString(), (int)state);
    }   
    public int GetRingSkinState(SkinType type)
    {
        return PlayerPrefs.GetInt("skin" + type.ToString(), 0);
    }






    public void SetBackground(int backgroundID)
    {
        PlayerPrefs.SetInt("background", backgroundID);
    }

    public int GetBackground()
    {
        return PlayerPrefs.GetInt("background");
    }
}
public enum ShopState
{
    UnBought,
    Bought,
    Equipped
}
