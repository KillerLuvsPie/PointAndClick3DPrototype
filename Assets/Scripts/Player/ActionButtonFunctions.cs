using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ActionButtonFunctions : MonoBehaviour
{
    //VARIABLES
    private string inputValue = "";

    //FUNCTIONS
    //ACTION BUTTON FUNCTIONS
    public void InsertInput(string value, TextMeshProUGUI display, TextMeshProUGUI confirmDisplay)
    {
        if(inputValue.Length < 9)
        {
            inputValue += value;
            display.text = inputValue;
            confirmDisplay.text = "";
            StartCoroutine(PlayInputSounds(SoundManager.Instance.sfx_keypadHit));
        }
        else
            StartCoroutine(PlayInputSounds(SoundManager.Instance.sfx_keypadWrong));
    }

    public void DeleteLastInput(TextMeshProUGUI display, TextMeshProUGUI confirmDisplay)
    {
        if(inputValue.Length > 0)
        {
            inputValue = inputValue.Remove(inputValue.Length-1);
            display.text = inputValue;
            confirmDisplay.text = "";
            StartCoroutine(PlayInputSounds(SoundManager.Instance.sfx_keypadHit));
        }
        else
            StartCoroutine(PlayInputSounds(SoundManager.Instance.sfx_keypadWrong));
    }

    public void VerifyInput(RobotController rc, TextMeshProUGUI checkDisplay, bool isDrone)
    {
        if(inputValue == rc.unlockCode)
        {
            StartCoroutine(PlayInputSounds(SoundManager.Instance.sfx_keypadCorrect));
            checkDisplay.text = "^";
            //SWITCH SIDE MENU FROM KEYPAD TO CONNECTIONS DISPLAY (IF IT'S A DRONE)
            if(isDrone)
            {
                StartCoroutine(UIManager.Instance.SwitchToConnections());
                rc.isHacked = true;
            }
            //OPEN STORE SHUTTERS (IF IT'S NOT A DRONE)
            else
                GameManager.Instance.OpenStore();
        }
        else
        {
            StartCoroutine(PlayInputSounds(SoundManager.Instance.sfx_keypadWrong));
            checkDisplay.text = "X";
        }  
    }

    public void ShowInterface(bool condition, GameObject menuToEnable, GameObject menuToDisable)
    {
        if(condition == true)
        {
            menuToEnable.SetActive(true);
            menuToDisable.SetActive(false);
        }
    }

    public void UnlockInteractible(GameObject obj)
    {
        obj.GetComponent<InteractibleObject>().ActivateObject();
        print(obj);
    }

    public void CloseMenu()
    {
        StartCoroutine(UIManager.Instance.SideMenuSlideOut());
    }

    private IEnumerator PlayInputSounds(AudioClip clip)
    {
        SoundManager.Instance.PlayOneSound(SoundManager.Instance.sfx_keypadButtons[Random.Range(0, SoundManager.Instance.sfx_keypadButtons.Length)], SoundManager.Instance.audioSource);
        if(clip != null)
        {
            yield return new WaitForSeconds(0.2f);
            SoundManager.Instance.PlayOneSound(clip, SoundManager.Instance.audioSource);
        }
    }
}
