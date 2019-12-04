using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEditor;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public SoundTrack[] music;
    public SoundTrack[] soundEffects;

    public Slider musicLevelSlider;
    public Slider effectLevelSlider;

    public float musicVolume;
    public float effectsVolume;

    [HideInInspector]
    public string currentlyPlaying = "";
    //[HideInInspector]
    //public string currentlyPlayingSoundEffect = "";

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

        foreach(SoundTrack soundTrack in music)
        {
            PrepareSoundTrack(soundTrack);

            if (soundTrack.name == "Main Menu")
            {
                soundTrack.Play();
                currentlyPlaying = "Main Menu";
            }
        }

        foreach(SoundTrack soundEffect in soundEffects)
        {
            PrepareSoundTrack(soundEffect);
        }

        EditorUtility.SetDirty(gameObject);
    }

    public void Start()
    {
        if (musicLevelSlider != null && effectLevelSlider != null)
        {
            AddSliders();
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
        SoundTrack soundTrack = Array.Find(music, s => s.name == name);
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
        SoundTrack soundTrack = Array.Find(music, s => s.name == currentlyPlaying);
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

    //public void StopSoundEffect(String name)
    //{
    //    SoundTrack soundTrack = Array.Find(music, s => s.name == name);
    //    if (soundTrack != null)
    //    {
    //        soundTrack.Stop();
    //        //currentlyPlayingSoundEffect = "";
    //    }
    //}

    //public void StopSoundEffect()
    //{
    //    StopSoundEffect(currentlyPlayingSoundEffect);
    //}

    //public void PlaySoundEffect(string name)
    //{
    //    if (currentlyPlayingSoundEffect == name)
    //        return;
    //    StopSoundEffect(currentlyPlayingSoundEffect);
    //    currentlyPlayingSoundEffect = "";
    //    SoundTrack soundTrack = Array.Find(music, s => s.name == name);
    //    if (soundTrack != null)
    //    {
    //        soundTrack.Play();
    //        currentlyPlayingSoundEffect = name;
    //    }
    //}

    public void SetVolume(float newVolume, SoundTrack[] soundTracks)
    {
        if (newVolume > 1 || newVolume < 0)
        {
            Debug.LogError("Volume out of bounds: " + newVolume);
            return;
        }
        
        foreach(SoundTrack soundTrack in soundTracks)
        {
            soundTrack.audioSource.volume = newVolume;
        }
    }

    private void PrepareSoundTrack(SoundTrack soundTrack)
    {
        soundTrack.audioSource = gameObject.AddComponent<AudioSource>();
        soundTrack.audioSource.clip = soundTrack.audioClip;
        soundTrack.audioSource.loop = soundTrack.loop;
        soundTrack.audioSource.volume = (musicLevelSlider != null)? musicLevelSlider.value : 1;
    }

    public void AddSliders()
    {
        StartCoroutine(WaitForSceneToLoadToAddSliders());
    }

    private IEnumerator WaitForSceneToLoadToAddSliders()
    {
        // This waiting is just for the scene to be loaded
        yield return new WaitForSeconds(0.1f);
        Slider[] sliders = FindObjectsOfType<Slider>();

        musicLevelSlider = Array.Find(sliders, slider => slider.name.Equals("Music Level Slider"));
        effectLevelSlider = Array.Find(sliders, slider => slider.name.Equals("Effects Level Slider"));

        musicLevelSlider
            .onValueChanged
            .AddListener(delegate 
            { 
                SetVolume(musicLevelSlider.value, music); 
                musicVolume = musicLevelSlider.value;  
            });
        musicLevelSlider.value = musicVolume;
        effectLevelSlider
            .onValueChanged
            .AddListener(delegate 
            { 
                SetVolume(effectLevelSlider.value, soundEffects); 
                effectsVolume = effectLevelSlider.value;  
            });
        effectLevelSlider.value = effectsVolume;
    }
}
