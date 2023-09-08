using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public List<ParticleSystem> winEff => GetComponentsInChildren<ParticleSystem>().ToList();  
    public void PlayEffect()
    {
        foreach(var e in winEff)
        {
            e.Play();
        }
    }

}
