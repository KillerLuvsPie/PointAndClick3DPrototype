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
    //ICON REFERENCES
    public Sprite garageIcon;
    public Sprite flyingDroneIcon;
    public Sprite shockDroneIcon;
    public Sprite cameraIcon;
    public Sprite lightIcon;
    //SIDE MENU VARIABLES
    public GameObject sideMenu;
    private RectTransform sideMenuRectT;
    public GameObject connectionPrefab;
    public Button[] keypadButtons;
    public ScrollRect connectionsDisplay;
    public Button closeSideMenuButton;
    public TextMeshProUGUI keypadNumberDisplay;
    public TextMeshProUGUI keypadConfirmDisplay;

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
    private void GetSideMenuButtons(RobotController rc)
    {
        Transform robotInterface = sideMenu.transform.GetChild(0);
        actionButtonFunctions = robotInterface.GetComponent<ActionButtonFunctions>();
        closeSideMenuButton = robotInterface.transform.GetChild(0).GetComponent<Button>();
        closeSideMenuButton.interactable = true;
        switch(rc.buttonGroup)
        {
            case DataVariables.RobotButtonGroup.FlyingDrone:
            case DataVariables.RobotButtonGroup.ShockDrone:
                keypadButtons = robotInterface.transform.GetChild(1).GetComponentsInChildren<Button>();
                keypadNumberDisplay = robotInterface.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
                keypadConfirmDisplay = robotInterface.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
                connectionsDisplay = robotInterface.transform.GetChild(2).GetChild(0).GetComponent<ScrollRect>();
                for(int i = 0; i < keypadButtons.Length; i++)
                    keypadButtons[i].interactable = true;
                for(int i = 0; i < rc.connections.Length; i++)
                {
                    GameObject connection = Instantiate(connectionPrefab, connectionsDisplay.content);
                    switch(rc.connections[i].buttonGroup)
                    {
                        case DataVariables.RobotButtonGroup.FlyingDrone:
                            connection.transform.GetChild(0).GetComponent<Image>().sprite = flyingDroneIcon;
                            break;
                        case DataVariables.RobotButtonGroup.ShockDrone:
                            connection.transform.GetChild(0).GetComponent<Image>().sprite = shockDroneIcon;
                            break;
                        case DataVariables.RobotButtonGroup.Camera:
                            connection.transform.GetChild(0).GetComponent<Image>().sprite = cameraIcon;
                            break;
                        case DataVariables.RobotButtonGroup.GarageKeypad:
                            connection.transform.GetChild(0).GetComponent<Image>().sprite = garageIcon;
                            break;
                        case DataVariables.RobotButtonGroup.Light:
                            connection.transform.GetChild(0).GetComponent<Image>().sprite = lightIcon; 
                            break;
                        default:
                            break;
                    }
                    connection.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = rc.connections[i].unlockCode;
                    //ADD CONNECTION BETWEEN RC OBJECT AND THE LOOP OBJECT HERE
                }
                //CYCLE THROUGH CONNECTIONS IN ROBOT CONTROLLER
                break;
            case DataVariables.RobotButtonGroup.Camera:
                keypadButtons = new Button[0];
                break;
            case DataVariables.RobotButtonGroup.GarageKeypad:
                keypadButtons = robotInterface.transform.GetChild(1).GetComponentsInChildren<Button>();
                keypadNumberDisplay = robotInterface.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
                keypadConfirmDisplay = robotInterface.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
                for(int i = 0; i < keypadButtons.Length; i++)
                    keypadButtons[i].interactable = true;
                break;
            default:
                print("Invalid robot button group: " + rc.buttonGroup + " on GetSideMenuButtons() function");
                break;
        }
    }

    private void AssignButtonFunctions(RobotController rc)
    {
        closeSideMenuButton.onClick.AddListener(() => actionButtonFunctions.CloseMenu());
        switch(rc.buttonGroup)
        {
            case DataVariables.RobotButtonGroup.FlyingDrone:
            case DataVariables.RobotButtonGroup.ShockDrone:
                for(int i = 0; i < keypadButtons.Length-2; i++)
                {
                    int cachedIndex = i;
                    keypadButtons[i].onClick.AddListener(() => actionButtonFunctions.InsertInput(keypadButtons[cachedIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text, keypadNumberDisplay, keypadConfirmDisplay));
                }
                keypadButtons[keypadButtons.Length-2].onClick.AddListener(() => actionButtonFunctions.DeleteLastInput(keypadNumberDisplay, keypadConfirmDisplay));
                keypadButtons[keypadButtons.Length-1].onClick.AddListener(() => actionButtonFunctions.VerifyInput(rc.unlockCode, keypadConfirmDisplay, true));
                break;
            case DataVariables.RobotButtonGroup.Camera:

                break;
            case DataVariables.RobotButtonGroup.GarageKeypad:
                for(int i = 0; i < keypadButtons.Length-2; i++)
                {
                    int cachedIndex = i;
                    keypadButtons[i].onClick.AddListener(() => actionButtonFunctions.InsertInput(keypadButtons[cachedIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text, keypadNumberDisplay, keypadConfirmDisplay));
                }
                keypadButtons[keypadButtons.Length-2].onClick.AddListener(() => actionButtonFunctions.DeleteLastInput(keypadNumberDisplay, keypadConfirmDisplay));
                keypadButtons[keypadButtons.Length-1].onClick.AddListener(() => actionButtonFunctions.VerifyInput(rc.unlockCode, keypadConfirmDisplay, false));
                break;
            default:
                print("Invalid robot button group: " + rc.buttonGroup + " on AssignButtonFunctions() function");
                break;
        }
    }

    private void DeactivateCloseButton()
    {
        closeSideMenuButton.interactable = false;
    }
    private void DeactivateButtons()
    {
        for(int i = 0; i < keypadButtons.Length; i++)
            keypadButtons[i].interactable = false;
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
        GetSideMenuButtons(robotController);
        AssignButtonFunctions(robotController);
    }

    //SIDE MENU SLIDE OUT ANIMATION (MIGHT BE REPLACED WITH AN ANIMATOR COMPONENT)
    public IEnumerator SideMenuSlideOut()
    {
        PlayerController.Instance.ControlToggle();
        DeactivateCloseButton();
        DeactivateButtons();
        while(sideMenuRectT.anchoredPosition.x < 250)
        {
            sideMenuRectT.anchoredPosition += new Vector2(2,0);
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator SwitchToConnections()
    {
        DeactivateButtons();
        yield return new WaitForSeconds(1);
        sideMenu.transform.GetChild(0).GetChild(1).GameObject().SetActive(false);
        sideMenu.transform.GetChild(0).GetChild(2).GameObject().SetActive(true);
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
