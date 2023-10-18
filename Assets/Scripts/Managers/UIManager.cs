using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject sideMenu;
    private RectTransform rectT;
    public Button[] commandButtons;
    //SIDE MENU SLIDE IN ANIMATION (MIGHT BE REPLACED WITH AN ANIMATOR COMPONENT)
    public IEnumerator SideMenuSlideIn()
    {
        while(rectT.anchoredPosition.x > -250)
        {
            rectT.anchoredPosition += new Vector2(-2,0);
            yield return new WaitForEndOfFrame();
        }
        GetSideMenuButtons();
        for(int i = 0; i < commandButtons.Length; i++)
        {
            commandButtons[i].interactable = true;
        }
    }
    //SIDE MENU SLIDE OUT ANIMATION (MIGHT BE REPLACED WITH AN ANIMATOR COMPONENT)
    public IEnumerator SideMenuSlideOut()
    {
        for(int i = 0; i < commandButtons.Length; i++)
        {
            commandButtons[i].interactable = false;
        }
        while(rectT.anchoredPosition.x < 250)
        {
            rectT.anchoredPosition += new Vector2(2,0);
            yield return new WaitForEndOfFrame();
        }
    }
    //CHECK IF SIDE MENU HAS A ROBOT INTERFACE AS A CHILD OBJECT
    public bool SideMenuHasInterface()
    {
        if(sideMenu.transform.childCount > 0)
            return true;
        return false;
    }
    //DELETE INTERFACE FROM OLD INTERACTION AND SHOW INTERFACE FROM NEW INTERACTION
    public void SideMenuReplaceInterface(GameObject obj)
    {
        if(SideMenuHasInterface())
            Destroy(sideMenu.transform.GetChild(0).GameObject());
        Instantiate(obj, sideMenu.transform.position, sideMenu.transform.rotation, sideMenu.transform);
    }

    //GET ACTION BUTTONS FROM NEW ROBOT INTERFACE
    private void GetSideMenuButtons()
    {
        Transform robotInterface = sideMenu.transform.GetChild(0);
        commandButtons = robotInterface.GetComponentsInChildren<Button>();
    }

    void Awake()
    {
        //SET SINGLETON
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
        rectT = sideMenu.GetComponent<RectTransform>();
    }
}
