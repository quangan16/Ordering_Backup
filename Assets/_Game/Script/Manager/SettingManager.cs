﻿using UnityEngine;
using UnityEngine.Audio;

namespace _Game.Script.Manager
{
    public class SettingManager : SingletonBehivour<SettingManager>
    {
        
        [SerializeField] private AudioMixerGroup MusicController;
        [SerializeField] private AudioMixerGroup SFXController;
        
        
        public bool IsMusicOn { get; set;}
        public bool IsSfxOn { get; set; }
        public bool IsVibrationOn { get; set; }

        public void Awake()
        {
            IsSfxOn = DataManager.Instance.GetSoundState() == 1;
            IsMusicOn = DataManager.Instance.GetMusicState() == 1;
            IsVibrationOn = false;
            DontDestroyOnLoad(this);
        }

        public void Start()
        {
           OnInit();
        }

        public void OnInit()
        {
            if (IsMusicOn)
            {
                MusicController.audioMixer.SetFloat("MusicVolume", 0.0f);
            }
            else
            {
                MusicController.audioMixer.SetFloat("MusicVolume", -80.0f);

            }

            if (IsSfxOn)
            {
                SFXController.audioMixer.SetFloat("SfxVolume", 0.0f);
            }
            else
            {
                SFXController.audioMixer.SetFloat("SfxVolume", -80.0f);
            }

            if (IsVibrationOn)
            {
                GameManager.isVibrate = true;
            }
            else
            {
                GameManager.isVibrate = false;
            }

        }

        public void MusicOn()
        {
            if (IsMusicOn)
            {
                MusicController.audioMixer.SetFloat("MusicVolume", 0.0f);
            }
           
        }

        public void MusicOff()
        {
            if (!IsMusicOn)
            {
                MusicController.audioMixer.SetFloat("MusicVolume", -80.0f);
            }
           
        }

        public void SfxOn()
        {
            if (IsSfxOn)
            {
                SFXController.audioMixer.SetFloat("SfxVolume", 0.0f);
            }

        }

        public void SfxOff()
        {
            if (!IsSfxOn)
            {
                SFXController.audioMixer.SetFloat("SfxVolume", -80.0f);
            }
           
        }



        public void VibrationOn()
        {
            if (IsVibrationOn)
            {
                GameManager.isVibrate = true;
            }
        }

        public void VibrationOff()
        {
            if (!IsVibrationOn)
            {
                GameManager.isVibrate = false;
            }
        }
        
        
    }
}