using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundTrack
{
    public string name;
    public AudioClip audioClip;
    public bool loop;

    //[Range(0f, 1f)]
    //public float volume = 1f; 

    [HideInInspector]
    public AudioSource audioSource;

    public void Play()
    {
        this.audioSource.Play();
    }

    public void Stop()
    {
        this.audioSource.Stop();
    }
}
