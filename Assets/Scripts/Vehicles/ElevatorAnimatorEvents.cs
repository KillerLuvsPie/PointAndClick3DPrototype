using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAnimatorEvents : MonoBehaviour
{
    public AudioSource audioSource;
    public void ElevatorSlideDoors()
    {
        SoundManager.Instance.PlayOneSound(SoundManager.Instance.sfx_elevatorSlideDoors, audioSource);
    }
    
    public void ElevatorCloseDoors()
    {
        SoundManager.Instance.PlayOneSound(SoundManager.Instance.sfx_elevatorCloseDoors, audioSource);
    }

    public void ElevatorOpenDoors()
    {
        SoundManager.Instance.PlayOneSound(SoundManager.Instance.sfx_elevatorOpenDoors, audioSource);
    }

    public void ElevatorStartMove()
    {
        SoundManager.Instance.PlayOneSound(SoundManager.Instance.sfx_elevatorStartMove, audioSource);
    }

    public void ElevatorStopMove()
    {
        audioSource.Stop();
        SoundManager.Instance.PlayOneSound(SoundManager.Instance.sfx_elevatorStopMove, audioSource);
    }

    public void ElevatorMoving()
    {
        SoundManager.Instance.PlayConstantSound(SoundManager.Instance.sfx_elevatorMoving, audioSource);
    }

    void Start()
    {
        SoundManager.Instance.allAudioSources.Add(audioSource);
    }
}
