using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonBehivour<DataManager>
{
    public Skin skins;
    public N_BackGround backGround;
    
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

    
    public SkinItem GetSkin(SkinType type)
    {
        return skins.GetSkinItem(type);
    }
    public void SetLastRingSkin(SkinType type) //last
    {
        PlayerPrefs.SetInt("LastSkin", (int)type);
    }

    public SkinType GetLastRingSkin() //last
    {
        return (SkinType)PlayerPrefs.GetInt("LastSkin", 0);
    }
    public void SetRingSkinState(ShopState state,SkinType type)
    {
        PlayerPrefs.SetInt("skin" + type.ToString(), (int)state);
    }   
    public ShopState GetRingSkinState(SkinType type)
    {
        return (ShopState)PlayerPrefs.GetInt("skin" + type.ToString(), 1);
    }




    public BackGroundItem GetBackGround(BackGroundType type)
    {
        return backGround.GetBackGround(type);
    }

    public void SetLastBackground(BackGroundType type)
    {
        PlayerPrefs.SetInt("background", (int)type);
    }

    public BackGroundType GetLastBackground()
    {
        return (BackGroundType)PlayerPrefs.GetInt("background",0);
    }

    public void SetBackGroundState(ShopState state, BackGroundType type)
    {
        PlayerPrefs.SetInt("Background"+type.ToString(),(int)state);
    }
    public ShopState GetBackGroundState(BackGroundType type)
    {
        return (ShopState)PlayerPrefs.GetInt("Background"+type.ToString(),0);
    }

    public void SetSoundState(int soundState)
    {
        PlayerPrefs.SetInt("Sound", soundState);
    }

    public int GetSoundState()
    {
        return PlayerPrefs.GetInt("Sound", 1);
    }


    public void SetMusicState(int musicState)
    {
        PlayerPrefs.SetInt("Music", musicState);
    }

    public int GetMusicState()
    {
        return PlayerPrefs.GetInt("Music", 1);
    }

    public void SetVibState(int vibState)
    {
        PlayerPrefs.SetInt("Vibration", vibState);
    }

    public int GetVibState()
    {
        return PlayerPrefs.GetInt("Vibration", 1);
    }

}
public enum ShopState
{
    Locked,
    UnBought,
    Bought,
    Equipped
}
