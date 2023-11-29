using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource audioSource;
    //SOUND REFERENCES
    public AudioClip[] sfx_keypadButtons;
    public AudioClip sfx_keypadHit;
    public AudioClip sfx_keypadWrong;
    public AudioClip sfx_keypadCorrect;
    public AudioClip[] sfx_droneSounds;
    public AudioClip sfx_droneRotorsIdle;
    
    //SOUND FUNCTIONS
    public void PlayConstantSound(AudioClip clip, AudioSource source, float volume = 1f)
    {
        source.clip = clip;
        source.volume = volume;
        source.Play();
    }

    public void PlayOneSound(AudioClip clip, AudioSource source, float volume = 1)
    {
        source.PlayOneShot(clip, volume);
    }

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        audioSource = GetComponent<AudioSource>();
    }
}
