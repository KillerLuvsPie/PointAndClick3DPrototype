using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public Robot robot;
    public GameObject robotInterface;
    public RobotController[] connections;
    public DataVariables.RobotButtonGroup buttonGroup = DataVariables.RobotButtonGroup.None;
    public string unlockCode;
    public bool isHacked = false;

    //SOUND VARIABLES
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    float minSoundWaitTime = 4, maxSoundWaitTime = 15;

    //COROUTINES
    //PLAY A RANDOM SOUND IN DRONE SOUNDS ARRAY (FLYING DRONE ONLY)
    private IEnumerator PlayRandomDroneSound()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minSoundWaitTime, maxSoundWaitTime));
            int i = Random.Range(0, SoundManager.Instance.sfx_droneSounds.Length);
            //ADJUST VOLUME ACCORDING TO CHOSEN CLIP
            float volume = 1;
            switch(i)
            {
                case 0:
                    volume = 0.6f;
                    break;
                case 1:
                    volume = 0.5f;
                    break;
                case 2:
                    volume = 0.9f;
                    break;
            }
            SoundManager.Instance.PlayOneSound(SoundManager.Instance.sfx_droneSounds[i], audioSource1, volume);
        }
    }
    //PLAY SHOCK SOUNDS RANDOMLY FOR A RANdOM AMOUNT OF TIME (SHOCKDRONE ONLY)
    private IEnumerator PlayShockSounds()
    {
        SoundManager.Instance.PlayConstantSound(SoundManager.Instance.sfx_electricitySounds, audioSource2);
        audioSource2.Pause();
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 5f));
            audioSource2.UnPause();
            //PLAY SHOCK PARTICLES HERE
            yield return new WaitForSecondsRealtime(Random.Range(0.25f, 0.75f));
            audioSource2.Pause();
            //STOP SHOCK PARTICLES HERE
        }
    }
    void Start()
    {
        robotInterface = robot.robotInterface;
        switch(buttonGroup)
        {
            case DataVariables.RobotButtonGroup.FlyingDrone:
                SoundManager.Instance.allAudioSources.Add(audioSource1);
                SoundManager.Instance.PlayConstantSound(SoundManager.Instance.sfx_droneRotorsIdle, audioSource1, audioSource1.volume);
                StartCoroutine(PlayRandomDroneSound());
                break;
            case DataVariables.RobotButtonGroup.ShockDrone:
                SoundManager.Instance.allAudioSources.Add(audioSource1);
                SoundManager.Instance.PlayConstantSound(SoundManager.Instance.sfx_shockdroneIdle, audioSource1, audioSource1.volume);
                StartCoroutine(PlayShockSounds());
                break;
        }
        if(buttonGroup == DataVariables.RobotButtonGroup.None)
            Debug.Log(name + " has no button group assigned");
    }
}
