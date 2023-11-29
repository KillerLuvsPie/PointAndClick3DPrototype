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
    private AudioSource audioSource;
    float minSoundWaitTime = 4, maxSoundWaitTime = 15;

    //COROUTINES
    //PLAY A RANDOM SOUND IN DRONE SOUNDS ARRAY
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
            SoundManager.Instance.PlayOneSound(SoundManager.Instance.sfx_droneSounds[i], audioSource, volume);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        robotInterface = robot.robotInterface;
        switch(buttonGroup)
        {
            case DataVariables.RobotButtonGroup.FlyingDrone:
                SoundManager.Instance.PlayConstantSound(SoundManager.Instance.sfx_droneRotorsIdle, audioSource, audioSource.volume);
                StartCoroutine(PlayRandomDroneSound());
                break;
            case DataVariables.RobotButtonGroup.ShockDrone:
                /*SoundManager.Instance.PlayConstantSound(SoundManager.Instance.sfx_shockdroneIdle, audioSource);*/
                break;
        }
        if(buttonGroup == DataVariables.RobotButtonGroup.None)
            Debug.Log(name + " has no button group assigned");
    }
}
