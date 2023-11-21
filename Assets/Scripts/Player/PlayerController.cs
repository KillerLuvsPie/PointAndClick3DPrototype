using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    //VARIABLES
    //SINGLETON
    public static PlayerController Instance;
    //NAVMESHAGENT
    public NavMeshAgent playerAgent;
    private float moveStopRadius = 0;
    //COROUTINE
    private IEnumerator coroutine;
    private bool interactionQueued = false;
    //PLAYER VARIABLES
    public Material mat;
    public bool isControlActive = true;
    //FUNCTIONS
    //FUNCTION USED TO STOP THE INTERACTION COROUTINE - RESET INTERACTION
    private void StopRunningCoroutine(IEnumerator c)
    {
        if(interactionQueued)
        {
            interactionQueued = false;
            StopCoroutine(c);
        }
    }
    //QUEUE A PLAYER INTERACTION (STOP CURRENT QUEUED ACTION, SET NAVMESH AGENT PARAMETERS AND THEN RUN COROUTINE)
    public void QueueInteraction(GameObject obj, float interactRadius, bool ignoreYPos)
    {
        if(isControlActive)
        {
            StopRunningCoroutine(coroutine);
            //print("Coroutine stopped at interaction");
            coroutine = WalkingToObject(obj, interactRadius);
            playerAgent.stoppingDistance = interactRadius;
            if(ignoreYPos)
                playerAgent.SetDestination(new Vector3(obj.transform.position.x, 0, obj.transform.position.z));
            else
                playerAgent.SetDestination(obj.transform.position);
            StartCoroutine(coroutine);
        }
    }
    //CHOOSE ACTION DEPENDING ON INTERACTIBLE OBJECT TAG
    private void InteractionTypeSelection(GameObject obj)
    {
        if(obj.GetComponent<InteractibleObject>().isActive)
        {
            switch (obj.tag)
            {
                case "NPC":             //NPC INTERACTION
                    print(obj.GetComponent<NPCController>().message);
                    break;
                case "Interactible":    //OBJECT INTERACTION
                    break;
                case "Vehicle":         //VEHICLE INTERACTION
                    break;
                case "Robot":           //ROBOT INTERACTION
                    UIManager.Instance.SideMenuReplaceInterface(obj.GetComponent<RobotController>().robotInterface);
                    StartCoroutine(UIManager.Instance.SideMenuSlideIn(obj.GetComponent<RobotController>()));
                    break;
                default:                //ERROR INTERACTION
                    print("Interaction could not find the tag: " + obj.tag);
                    break;
            }   
        }
        
    }

    //ENABLE / DISABLE CONTROLS
    public void ControlToggle()
    {
        isControlActive = !isControlActive;
    }

    //COROUTINES
    //WAITS FOR PLAYER TO BE IN RANGE OF OBJECT AND THEN INTERACTS WITH SAID OBJECT
    private IEnumerator WalkingToObject(GameObject obj, float interactRadius)
    {
        interactionQueued = true;
        //print("Walk start");
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => playerAgent.remainingDistance <= interactRadius);
        interactionQueued = false;
        //print("Walk finish");
        InteractionTypeSelection(obj);
    }

    //ENGINE FUNCTIONS
    void Awake()
    {
        //SET SINGLETON
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        playerAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            if(Input.GetMouseButtonDown(0) && isControlActive)
            {
                Ray movePos = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(movePos, out var hitInfo))
                {
                    if(hitInfo.collider.CompareTag("Geometry"))
                    {
                        StopRunningCoroutine(coroutine);
                        //print("Coroutine stopped at geometry click");
                        playerAgent.stoppingDistance = moveStopRadius;
                        playerAgent.SetDestination(hitInfo.point);
                    }
                }
            }  
        }
        //CHECK IF PLAYER IS MOVING - MOCK ANIMATOR
        if(playerAgent.velocity == Vector3.zero)
            mat.color = new Color(0,1,0,1);
        else
        {
            mat.color = new Color(playerAgent.velocity.magnitude/playerAgent.speed,1,0,1);
        }
    }
}