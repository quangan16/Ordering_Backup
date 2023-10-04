using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonBehivour<SoundManager>
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    public void PlaySound(AudioClip audioClip)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }
    public void ChangeVolume(float volume)
    { 
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
    }
    
    public void Play()
    {
        audioSource.Play();
    }

    public void PlayButtonSound()
    {
        PlaySound(audioClip);
    }
}
