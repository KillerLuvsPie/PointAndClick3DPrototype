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
    public Material mat;
    //FUNCTIONS
    private void StopRunningCoroutine(IEnumerator c)
    {
        if(interactionQueued)
        {
            interactionQueued = false;
            StopCoroutine(c);
        }
    }
    
    public void QueueInteraction(GameObject obj, string npcMessage, float interactRadius)
    {
        StopRunningCoroutine(coroutine);
        print("Coroutine stopped at interaction");
        coroutine = WalkingToNPC(npcMessage, interactRadius);
        playerAgent.stoppingDistance = interactRadius;
        playerAgent.SetDestination(obj.transform.position);
        StartCoroutine(coroutine);
    }
    //COROUTINES
    private IEnumerator WalkingToNPC(string npcMessage, float interactRadius)
    {
        interactionQueued = true;
        print("Walk start");
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => playerAgent.remainingDistance <= interactRadius);
        interactionQueued = false;
        print("Walk finish");
        print(npcMessage);
        StartCoroutine(UIManager.Instance.SideMenuSlideIn());
    }
    //ENGINE FUNCTIONS
    void Awake()
    {
        //SET SINGLETON
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    void Update()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray movePos = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(movePos, out var hitInfo))
                {
                    if(hitInfo.collider.CompareTag("Geometry"))
                    {
                        StopRunningCoroutine(coroutine);
                        print("Coroutine stopped at geometry click");
                        playerAgent.stoppingDistance = moveStopRadius;
                        playerAgent.SetDestination(hitInfo.point);
                    }
                }
            }  
        }
        //CHECK IF PLAYER IS MOVING
        if(playerAgent.velocity == Vector3.zero)
            mat.color = new Color(0,1,0,1);
        else
            mat.color = new Color(1,0,0,1);
    }
}