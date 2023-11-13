using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //SINGLETON
    public static UIManager Instance;
    private ActionButtonFunctions actionButtonFunctions;
    //CLICK INDICATOR REFERENCES
    public GameObject clickIndicatorPrefab;
    public Transform clickIndicators;
    //SIDE MENU VARIABLES
    public GameObject sideMenu;
    private RectTransform sideMenuRectT;
    public TextMeshProUGUI sideMenuTooltip;
    public Button[] commandButtons;

    //FUNCTIONS
    
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
        actionButtonFunctions = robotInterface.GetComponent<ActionButtonFunctions>();
        commandButtons = robotInterface.GetComponentsInChildren<Button>();
        sideMenuTooltip = robotInterface.GetChild(robotInterface.childCount - 1).GetComponent<TextMeshProUGUI>();
    }

    private void AssignButtonFunctions(DataVariables.RobotButtonGroup btnGrp, GameObject obj)
    {
        switch(btnGrp)
        {
            case DataVariables.RobotButtonGroup.Drone1:
                commandButtons[0].onClick.AddListener(() => actionButtonFunctions.UnlockInteractible(obj));
                commandButtons[0].GetComponent<OnHoverButton>().tooltip = "Unlock " + obj.name;
                print(commandButtons[0].onClick.GetPersistentEventCount());
                break;
            case DataVariables.RobotButtonGroup.Drone2:
                commandButtons[0].onClick.AddListener(() => actionButtonFunctions.UnlockInteractible(obj));
                break;
            case DataVariables.RobotButtonGroup.Drone3:
                commandButtons[0].onClick.AddListener(() => actionButtonFunctions.UnlockInteractible(obj));
                break;
            default:
                print("Invalid robot button group: " + btnGrp);
                break;
        }
    }

    //COROUTINES
    //SIDE MENU SLIDE IN ANIMATION (MIGHT BE REPLACED WITH AN ANIMATOR COMPONENT)
    public IEnumerator SideMenuSlideIn(RobotController robotController)
    {
        PlayerController.Instance.ControlToggle();
        while(sideMenuRectT.anchoredPosition.x > -250)
        {
            sideMenuRectT.anchoredPosition += new Vector2(-2,0);
            yield return new WaitForEndOfFrame();
        }
        GetSideMenuButtons();
        AssignButtonFunctions(robotController.buttonGroup, robotController.robotTarget.GameObject());
        for(int i = 0; i < commandButtons.Length; i++)
        {
            commandButtons[i].interactable = true;
        }
    }

    //SIDE MENU SLIDE OUT ANIMATION (MIGHT BE REPLACED WITH AN ANIMATOR COMPONENT)
    public IEnumerator SideMenuSlideOut()
    {
        PlayerController.Instance.ControlToggle();
        for(int i = 0; i < commandButtons.Length; i++)
        {
            commandButtons[i].interactable = false;
        }
        while(sideMenuRectT.anchoredPosition.x < 250)
        {
            sideMenuRectT.anchoredPosition += new Vector2(2,0);
            yield return new WaitForEndOfFrame();
        }
    }

    //UNITY FUNCTIONS
    void Awake()
    {
        //SET SINGLETON
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
        sideMenuRectT = sideMenu.GetComponent<RectTransform>();
    }
}
