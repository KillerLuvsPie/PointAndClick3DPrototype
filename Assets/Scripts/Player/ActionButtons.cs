using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtons : MonoBehaviour
{
    public void CallRobot(GameObject obj)
    {
        GameObject instance = Instantiate(obj, GameManager.Instance.actorWrapper);

    }

    public void ActionButtonDefault()
    {
        StartCoroutine(UIManager.Instance.SideMenuSlideOut());
    }
}
