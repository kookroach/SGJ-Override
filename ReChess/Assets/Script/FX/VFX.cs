using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class VFX : MonoBehaviour
{
    //public ParticleSystem particleSystem;


    public void playVFX()
    {
        GetComponent<ParticleSystem>().Play();
    }
}
