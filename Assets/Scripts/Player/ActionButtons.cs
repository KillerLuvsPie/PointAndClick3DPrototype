using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtons : MonoBehaviour
{
    public void ActionButton1()
    {
        StartCoroutine(UIManager.Instance.SideMenuSlideOut());
    }
}
