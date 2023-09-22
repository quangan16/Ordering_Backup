using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingGUI : PopupManager
{

    [SerializeField] private GameObject SoundOn;
    [SerializeField] private GameObject SoundOff;
    [SerializeField] private bool isSoundOn;

    [SerializeField] private GameObject MusicOn;
    [SerializeField] private GameObject MusicOff;
    [SerializeField] private bool isMusicOn;

    [SerializeField] private GameObject VibOn;
    [SerializeField] private GameObject VibOff;
    [SerializeField] private bool isVibOn;

    private new void OnEnable()
    {
        OnOpen();
        Init();
    }

    void Init()
    {
        isSoundOn = DataManager.Instance.GetSoundState() == 1;
        isMusicOn = DataManager.Instance.GetMusicState() == 1;
        isVibOn = DataManager.Instance.GetVibState() == 1;
        SoundOn.SetActive(isSoundOn);
        SoundOff.SetActive(!isSoundOn);
        
        MusicOn.SetActive(isMusicOn);
        MusicOff.SetActive(!isMusicOn);
        VibOn.SetActive(isVibOn);
        VibOff.SetActive(!isVibOn);
        
    }
    public void OpenSettings()
    {
        gameObject.SetActive(true);
    }

    public void ConfirmSettings()
    {
        DataManager.Instance.SetSoundState(Convert.ToInt16(isSoundOn));
        DataManager.Instance.SetMusicState(Convert.ToInt16(isMusicOn));
        DataManager.Instance.SetVibState(Convert.ToInt16(isVibOn));
        OnClose();
    }
    
    public void ToggleSound()
    {
        if (isSoundOn)
        {
            isSoundOn = false;
            SoundOn.SetActive(isSoundOn);
        }
        else
        {
            isSoundOn = true;
            SoundOn.SetActive(isSoundOn);
        }
        SoundOff.SetActive(!isSoundOn);
    }

   

   
    public void ToggleMusic()
    {
        if (isMusicOn)
        {
            isMusicOn = false;
            MusicOn.SetActive(isMusicOn);
        }
        else
        {
            isMusicOn = true;
            MusicOn.SetActive(isMusicOn);
        }

        MusicOff.SetActive(!isMusicOn);
    }

  
    public void ToggleVib()
    {
        if (isVibOn)
        {
            isVibOn = false;
            VibOn.SetActive(isVibOn);
        }
        else
        {
            isVibOn = true;
            VibOn.SetActive(isVibOn);
        }

        VibOff.SetActive(!isVibOn);
    }
    
    
}
