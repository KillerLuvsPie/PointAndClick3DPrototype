using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDoorAnimatorEvents : MonoBehaviour
{
    private AudioSource audioSource;

    public void GarageOpen()
    {
        SoundManager.Instance.PlayOneSound(SoundManager.Instance.sfx_garageStartOpen, audioSource);
    }

    public void GarageOpening()
    {
        SoundManager.Instance.PlayConstantSound(SoundManager.Instance.sfx_garageOpening, audioSource);
    }

    public void GarageStop()
    {
        audioSource.Stop();
        SoundManager.Instance.PlayOneSound(SoundManager.Instance.sfx_garageStopOpen, audioSource);
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        SoundManager.Instance.allAudioSources.Add(audioSource);
    }
}
