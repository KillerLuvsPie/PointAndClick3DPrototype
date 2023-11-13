using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonFunctions : MonoBehaviour
{
    public void UnlockInteractible(GameObject obj)
    {
        obj.GetComponent<InteractibleObject>().ActivateObject();
        print(obj);
    }

    public void ActionButtonDefault()
    {
        StartCoroutine(UIManager.Instance.SideMenuSlideOut());
    }
}
