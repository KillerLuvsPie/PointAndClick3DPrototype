using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class OnHoverButton : MonoBehaviour
{
    public string tooltip = "";

    public void ShowTooltip()
    {
        UIManager.Instance.sideMenuTooltip.text = tooltip;
        print(tooltip);
    }

    public void ClearTooltip()
    {
        UIManager.Instance.sideMenuTooltip.text = "";
    }
}
