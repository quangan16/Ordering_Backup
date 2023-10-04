using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Script.Manager;
using UnityEngine;
using UnityEngine.UI;

public class SettingGUI : PopupManager
{

    [SerializeField] private GameObject SoundOn;
    [SerializeField] private GameObject SoundOff;
   

    [SerializeField] private GameObject MusicOn;
    [SerializeField] private GameObject MusicOff;
  

    [SerializeField] private GameObject VibOn;
    [SerializeField] private GameObject VibOff;
  

    private new void OnEnable()
    {
        OnOpen();
        Init();
    }

    void Init()
    {
       
        SoundOn.SetActive(SettingManager.Instance.IsSfxOn);
        SoundOff.SetActive(!SettingManager.Instance.IsSfxOn);
        
        MusicOn.SetActive(SettingManager.Instance.IsMusicOn);
        MusicOff.SetActive(!SettingManager.Instance.IsMusicOn);
        VibOn.SetActive(SettingManager.Instance.IsVibrationOn);
        VibOff.SetActive(!SettingManager.Instance.IsVibrationOn);
        
    }
    public void OpenSettings()
    {
        gameObject.SetActive(true);
    }

    public void ConfirmSettings()
    {
        DataManager.Instance.SetSoundState(Convert.ToInt16(SettingManager.Instance.IsSfxOn));
        DataManager.Instance.SetMusicState(Convert.ToInt16(SettingManager.Instance.IsMusicOn));
        DataManager.Instance.SetVibState(Convert.ToInt16(SettingManager.Instance.IsVibrationOn));
        OnClose();
    }
    
    public void ToggleSound()
    {
        if (SettingManager.Instance.IsSfxOn)
        {
            SettingManager.Instance.IsSfxOn = false;
            SoundOn.SetActive(SettingManager.Instance.IsSfxOn);
            SettingManager.Instance.SfxOff();
        }
        else
        {
            SettingManager.Instance.IsSfxOn = true;
            SoundOn.SetActive(SettingManager.Instance.IsSfxOn);
            SettingManager.Instance.SfxOn();
        }
        SoundOff.SetActive(!SettingManager.Instance.IsSfxOn);
    }

   

   
    public void ToggleMusic()
    {
        if (SettingManager.Instance.IsMusicOn)
        {
            SettingManager.Instance.IsMusicOn = false;
            MusicOn.SetActive(SettingManager.Instance.IsMusicOn);
            SettingManager.Instance.MusicOff();
        }
        else
        {
            SettingManager.Instance.IsMusicOn = true;
            MusicOn.SetActive(SettingManager.Instance.IsMusicOn);
            SettingManager.Instance.MusicOn();
        }

        MusicOff.SetActive(!SettingManager.Instance.IsMusicOn);
    }

  
    public void ToggleVib()
    {
        if (SettingManager.Instance.IsVibrationOn)
        {
            SettingManager.Instance.IsVibrationOn = false;
            VibOn.SetActive(SettingManager.Instance.IsVibrationOn);
           
        }
        else
        {
            SettingManager.Instance.IsVibrationOn = true;
            VibOn.SetActive(SettingManager.Instance.IsVibrationOn);
            
        }
        SettingManager.Instance.Vibration();
        VibOff.SetActive(!SettingManager.Instance.IsVibrationOn);
    }
    
    
}
