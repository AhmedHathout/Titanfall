using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustMovementVolume : MonoBehaviour
{
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;
        GetComponent<AudioSource>().volume = audioManager.effectsVolume;
    }
}
