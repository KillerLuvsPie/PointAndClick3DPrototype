using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class NPCController : MonoBehaviour
{
    public string message;
    public bool isRoaming;
    private DataVariables.ElevatorNPCState state;
    private NavMeshAgent nma;
    public int queuePosition;

    public void MoveTo(Vector3 position)
    {
        nma.SetDestination(position);
    }

    //PROCESS FUNCTION
    private IEnumerator ProcessAction()
    {
        bool stop = false;
        while(!stop)
        {
            switch(state)
            {
                case DataVariables.ElevatorNPCState.Spawn:
                    state = DataVariables.ElevatorNPCState.Move;
                    Vector3 pos = new Vector3 (ElevatorManager.Instance.npcWaitPosition.position.x + (ElevatorManager.Instance.npcWaitOffset * ElevatorManager.Instance.elevatorQueue.Count), ElevatorManager.Instance.npcWaitPosition.position.y, ElevatorManager.Instance.npcWaitPosition.position.z);
                    MoveTo(pos);
                    queuePosition = ElevatorManager.Instance.elevatorQueue.Count;
                    ElevatorManager.Instance.elevatorQueue.Add(gameObject);
                    print(pos);
                    break;
                case DataVariables.ElevatorNPCState.Move:
                    yield return new WaitForEndOfFrame();
                    yield return new WaitUntil(() => nma.isStopped);
                    state = DataVariables.ElevatorNPCState.Adjust;
                    break;
                case DataVariables.ElevatorNPCState.Adjust:
                    yield return new WaitForEndOfFrame();
                    yield return new WaitUntil(() => nma.isStopped);
                    break;
                case DataVariables.ElevatorNPCState.Wait:
                    break;
                case DataVariables.ElevatorNPCState.Enter:
                    break;
                default:
                print("Could not find state for roaming npc " + gameObject.name);
                    break;
            }
        }
    }

    void Start()
    {
        if(isRoaming)
        {
            nma = GetComponent<NavMeshAgent>();
            state = DataVariables.ElevatorNPCState.Spawn;
            StartCoroutine(ProcessAction());
        }
        else
            state = DataVariables.ElevatorNPCState.Idle;
    }
}
