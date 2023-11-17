using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionButtonFunctions : MonoBehaviour
{
    private string inputValue = "";
    public void InsertInput(string value, TextMeshProUGUI display)
    {
        if(inputValue.Length < 10)
        {
            inputValue += value;
            display.text = inputValue;
        }
    }

    public void DeleteLastInput(TextMeshProUGUI display)
    {
        if(inputValue.Length > 0)
        {
            inputValue = inputValue.Remove(0, inputValue.Length-1);
            display.text = inputValue;
        }
    }

    public bool VerifyInput(string value, TextMeshProUGUI checkDisplay)
    {
        if(inputValue == value)
        {
            checkDisplay.text = "^";
            return true;
        }
        else
        {
            checkDisplay.text = "X";
            return false;
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
