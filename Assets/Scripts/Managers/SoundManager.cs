using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource audioSource;
    //SOUND REFERENCES
    //AMBIENT
    public AudioClip sfx_ambient;

    //KEYPAD
    public AudioClip[] sfx_keypadButtons;
    public AudioClip sfx_keypadHit;
    public AudioClip sfx_keypadWrong;
    public AudioClip sfx_keypadCorrect;

    //DRONES
    public AudioClip[] sfx_droneSounds;
    public AudioClip sfx_droneRotorsIdle;
    public AudioClip sfx_shockdroneIdle;
    public AudioClip sfx_electricitySounds;

    //ELEVATOR
    public AudioClip sfx_elevatorSlideDoors;
    public AudioClip sfx_elevatorCloseDoors;
    public AudioClip sfx_elevatorOpenDoors;
    public AudioClip sfx_elevatorStartMove;
    public AudioClip sfx_elevatorStopMove;
    public AudioClip sfx_elevatorMoving;

    //PEOPLE
    public AudioClip[] sfx_footsteps;
    
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
        audioSource.clip = sfx_ambient;
        audioSource.Play();
    }
}
