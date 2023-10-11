using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    //VARIABLES
    //SINGLETON
    public static PlayerController Instance;
    //NAVMESHAGENT
    public NavMeshAgent agent;
    //COROUTINE
    private IEnumerator coroutine;
    private bool interactionQueued = false;

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
        coroutine = WalkingToNPC(npcMessage, interactRadius);
        agent.stoppingDistance = interactRadius;
        agent.SetDestination(obj.transform.position);
        StartCoroutine(coroutine);
    }
    //COROUTINES
    private IEnumerator WalkingToNPC(string npcMessage, float interactRadius)
    {
        interactionQueued = true;
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => agent.remainingDistance <= interactRadius);
        interactionQueued = false;
        print(npcMessage);
    }
    //ENGINE FUNCTIONS
    void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            StopRunningCoroutine(coroutine);
            Ray movePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(movePos, out var hitInfo))
            {
                agent.stoppingDistance = 0;
                agent.SetDestination(hitInfo.point);
            }
        }
    }
}
