using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //SINGLETON
    public static UIManager Instance;

    private GameObject dialogueInstance;
    public GameObject canvas;
    private ActionButtonFunctions actionButtonFunctions;
    public Animator blackScreenAnim;
    public GameObject dialogueBaloon;

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
    private void FillConnectionsDisplay(RobotController[] connections, Vector3 startPos)
    {
        for(int i = 0; i < connections.Length; i++)
        {
            GameObject connection = Instantiate(connectionPrefab, connectionsDisplay.content);
            switch(connections[i].buttonGroup)
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
            connection.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = connections[i].unlockCode;
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            int cachedI = i;
            entry.callback.AddListener((data) => {connection.GetComponent<ConnectionController>().SetLine(startPos, connections[cachedI].transform.position);});
            connection.GetComponent<EventTrigger>().triggers.Add(entry);
            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener((data) => {connection.GetComponent<ConnectionController>().DeactivateLine();});
            connection.GetComponent<EventTrigger>().triggers.Add(entry);
            //ADD CONNECTION LINE BETWEEN RC OBJECT AND THE LOOP OBJECT HERE
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
    private void GetSideMenuButtons(RobotController rc)
    {
        Transform robotInterface = sideMenu.transform.GetChild(0);
        actionButtonFunctions = robotInterface.GetComponent<ActionButtonFunctions>();
        closeSideMenuButton = robotInterface.transform.GetChild(0).GetComponent<Button>();
        switch(rc.buttonGroup)
        {
            case DataVariables.RobotButtonGroup.FlyingDrone:
            case DataVariables.RobotButtonGroup.ShockDrone:
                keypadButtons = robotInterface.transform.GetChild(1).GetComponentsInChildren<Button>();
                keypadNumberDisplay = robotInterface.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
                keypadConfirmDisplay = robotInterface.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
                connectionsDisplay = robotInterface.transform.GetChild(2).GetChild(0).GetComponent<ScrollRect>();
                FillConnectionsDisplay(rc.connections, rc.transform.position);
                break;
            case DataVariables.RobotButtonGroup.Camera:
                keypadButtons = new Button[0];
                connectionsDisplay = robotInterface.transform.GetChild(1).GetComponent<ScrollRect>();
                FillConnectionsDisplay(rc.connections, rc.transform.position);
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

    public IEnumerator Dialogue(Vector3 targetPos, string npcMessage, string playerMessage)
    {
        GameObject dialogueInstance1 = Instantiate(dialogueBaloon, canvas.transform);
        dialogueInstance1.transform.position = Camera.main.WorldToScreenPoint(targetPos + Vector3.up * 5);
        dialogueInstance1.GetComponent<TextMeshProUGUI>().text = npcMessage;
        yield return new WaitForSeconds(5);
        Destroy(dialogueInstance1);
        GameObject dialogueInstance2 = Instantiate(dialogueBaloon, canvas.transform);
        dialogueInstance2.transform.position = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position + Vector3.up * 5);
        dialogueInstance2.GetComponent<TextMeshProUGUI>().text = playerMessage;
        yield return new WaitForSeconds(5);
        Destroy(dialogueInstance2);
    }
    private void AssignButtonFunctions(RobotController rc)
    {
        closeSideMenuButton.onClick.AddListener(() => actionButtonFunctions.CloseMenu());
        closeSideMenuButton.interactable = true;
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
                keypadButtons[keypadButtons.Length-1].onClick.AddListener(() => actionButtonFunctions.VerifyInput(rc, keypadConfirmDisplay, true));
                for(int i = 0; i < keypadButtons.Length; i++)
                    keypadButtons[i].interactable = true;
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
                keypadButtons[keypadButtons.Length-1].onClick.AddListener(() => actionButtonFunctions.VerifyInput(rc, keypadConfirmDisplay, false));
                for(int i = 0; i < keypadButtons.Length; i++)
                    keypadButtons[i].interactable = true;
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
        yield return new WaitForEndOfFrame();
        PlayerController.Instance.ControlToggle();
        GetSideMenuButtons(robotController);
        if(robotController.isHacked)
            ShowConnections();
        while(sideMenuRectT.anchoredPosition.x > -250)
        {
            sideMenuRectT.anchoredPosition += new Vector2(-500 * Time.deltaTime * 2, 0);
            yield return new WaitForEndOfFrame();
        }
        sideMenuRectT.anchoredPosition = new Vector2(-250, 0);
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
            sideMenuRectT.anchoredPosition += new Vector2(500 * Time.deltaTime * 2, 0);
            yield return new WaitForEndOfFrame();
        }
        sideMenuRectT.anchoredPosition = new Vector2(250, 0);
    }

    private void ShowConnections()
    {
        sideMenu.transform.GetChild(0).GetChild(1).GameObject().SetActive(false);
        sideMenu.transform.GetChild(0).GetChild(2).GameObject().SetActive(true);
    }
    
    public IEnumerator SwitchToConnections()
    {
        DeactivateButtons();
        yield return new WaitForSeconds(1);
        ShowConnections();
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
