using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFX : MonoBehaviour
{
    public AudioSource source; 
     void Awake()
     {
        source = GetComponent<AudioSource>();
     }


    public void PlaySound(AudioClip clip, bool loop = false, bool spatial = true)
    {
        source.loop = loop;
        source.spatialBlend = 1;
        source.spatialize = spatial;

        source.clip = clip;
        source.Play();
    }

   
}
