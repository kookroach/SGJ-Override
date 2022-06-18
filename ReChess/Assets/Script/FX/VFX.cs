using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class VFX : MonoBehaviour
{
    public ParticleSystem particleSystem;

    void Start()
    {
        particleSystem.GetComponent<ParticleSystem>();   
    }

    public void playVFX()
    {
        particleSystem.Play();
    }
}
