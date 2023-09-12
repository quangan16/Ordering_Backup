using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] AudioSource audioSource => GetComponent<AudioSource>();
    public List<ParticleSystem> winEff => GetComponentsInChildren<ParticleSystem>().ToList();  
    public void PlayEffect()
    {
        audioSource.Play();
        foreach(var e in winEff)
        {
            e.Play();
        }
    }

}
