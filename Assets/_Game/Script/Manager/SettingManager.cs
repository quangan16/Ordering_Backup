using UnityEngine;
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
            IsVibrationOn = DataManager.Instance.GetVibState() == 1;
            DontDestroyOnLoad(this);
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