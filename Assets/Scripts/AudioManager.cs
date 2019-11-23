using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public SoundTrack[] soundTracks;
    private static AudioManager instance;

    [HideInInspector]
    public string currentlyPlaying = "";
    [HideInInspector]
    public string currentlyPlayingSoundEffect = "";

    private volatile bool isMuted = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(SoundTrack soundTrack in soundTracks)
        {
            soundTrack.audioSource = gameObject.AddComponent<AudioSource>();
            soundTrack.audioSource.clip = soundTrack.audioClip;
            soundTrack.audioSource.loop = soundTrack.loop;

            if (soundTrack.name == "Main Menu")
            {
                soundTrack.Play();
                currentlyPlaying = "Main Menu";
            }
        }

    }

    private void Update()
    {
        if (isMuted)
        {
            AudioListener.pause = true;
        }
        else
        {
            AudioListener.pause = false;
        }
    }

    public void Play(string name, bool stopCurrentlyPlaying)
    {
        // If it is the same music, do nothing
        if (currentlyPlaying.Equals(name))
        {
            return;
        }

        if (stopCurrentlyPlaying)
        {
            StopCurrentlyPlaying();
        }
        SoundTrack soundTrack = Array.Find(soundTracks, s => s.name == name);
        if (soundTrack != null)
        {
            soundTrack.Play();
            if (stopCurrentlyPlaying)
            {
                currentlyPlaying = name;
            }
        }

        else
        {
            Debug.LogWarning("Track " + name + " Not found");
        }
    }

    public void Play(string name)
    {
        Play(name, true);
    }

    public void StopCurrentlyPlaying()
    {
        SoundTrack soundTrack = Array.Find(soundTracks, s => s.name == currentlyPlaying);
        if (soundTrack != null)
        {
            soundTrack.Stop();
            currentlyPlaying = "";
        }
        else
        {
            Debug.LogWarning("Could not find the currently playing track: " + currentlyPlaying);
        }
    }

    public void StopSoundEffect(String name)
    {
        SoundTrack soundTrack = Array.Find(soundTracks, s => s.name == name);
        if (soundTrack != null)
        {
            soundTrack.Stop();
            //currentlyPlayingSoundEffect = "";
        }
    }

    public void StopSoundEffect()
    {
        StopSoundEffect(currentlyPlayingSoundEffect);
    }

    public void PlaySoundEffect(string name)
    {
        if (currentlyPlayingSoundEffect == name)
            return;
        StopSoundEffect(currentlyPlayingSoundEffect);
        currentlyPlayingSoundEffect = "";
        SoundTrack soundTrack = Array.Find(soundTracks, s => s.name == name);
        if (soundTrack != null)
        {
            soundTrack.Play();
            currentlyPlayingSoundEffect = name;
        }
    }

    public void toggleMute()
    {
        isMuted = !isMuted;
        Debug.Log("Mute: " + isMuted);
    }
}
