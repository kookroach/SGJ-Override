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


    public void PlaySound(AudioClip clip, bool loop = false, bool spatial = true, bool destroyable = true)
    {
        source.loop = loop;
        source.spatialBlend = 1;
        source.spatialize = spatial;

        source.clip = clip;
        source.Play();
        if(destroyable)
            StartCoroutine(TimeToDie(source.clip.length));
    }

    IEnumerator TimeToDie(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
   
}
