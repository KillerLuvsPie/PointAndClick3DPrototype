using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionButtonFunctions : MonoBehaviour
{
    private string inputValue = "";
     

    public void InsertInput(string value, TextMeshProUGUI display, TextMeshProUGUI confirmDisplay)
    {
        if(inputValue.Length < 9)
        {
            inputValue += value;
            display.text = inputValue;
            confirmDisplay.text = "";
        }
    }

    public void DeleteLastInput(TextMeshProUGUI display, TextMeshProUGUI confirmDisplay)
    {
        if(inputValue.Length > 0)
        {
            inputValue = inputValue.Remove(inputValue.Length-1);
            display.text = inputValue;
            confirmDisplay.text = "";
        }
    }

    public void VerifyInput(string value, TextMeshProUGUI checkDisplay, bool isDrone)
    {
        if(inputValue == value)
        {
            checkDisplay.text = "^";
            if(isDrone)
                StartCoroutine(UIManager.Instance.SwitchToConnections());
            else
            {
                //PUT LEVEL COMPLETE HERE
            }
        }
        else
        {
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
}
