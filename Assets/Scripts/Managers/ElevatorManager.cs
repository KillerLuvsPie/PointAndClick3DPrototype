using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    //SINGLETON
    public static ElevatorManager Instance;
    //ELEVATOR ANIMATOR
    public Animator elevatorAnim;
    //ANIMATION VARIABLES
    public bool isMoving = false;
    public Transform elevatorPosition;
    //NPC PREFAB
    public GameObject roamingNPC;
    //NPC WRAPPER
    public Transform actorWrapper;
    //POSITiONS
    public Transform npcWaitPosition;
    public float npcWaitOffset =  2f;
    public List<Transform> spawnPoints;
    //NPC LIST
    public List<NPCController> elevatorQueue = new List<NPCController>();
    //SPAWN TIMER
    private int waitTimer = 5;

    //SPAWN FUNCTION
    private void SpawnNPC()
    {
        GameObject npc = Instantiate(roamingNPC, spawnPoints[Random.Range(0,spawnPoints.Count)].position , Quaternion.identity);
        npc.transform.parent = actorWrapper;
    }

    //ADJUST NPC QUEUE
    public void NPCQueueAdjustment()
    {
        for(int i = 0; i < elevatorQueue.Count; i++)
        {
            elevatorQueue[i].queuePosition--;
            elevatorQueue[i].MoveTo(npcWaitPosition.position);
        }
    }
    
    //COROUTINES
    //START COROUTINE FUNCTIONS
    public void StartElevatorCoroutine(NPCController npc)
    {
        StartCoroutine(MoveElevator(npc));
    }

    //ELEVATOR ANIMATION SEQUENCE COROUTINE
    private IEnumerator MoveElevator(NPCController npc)
    {
        isMoving = true;
        elevatorAnim.SetBool("areDoorsOpen", false);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => elevatorAnim.GetCurrentAnimatorStateInfo(elevatorAnim.GetLayerIndex("Door")).normalizedTime >= 1);
        npc.Delete();
        elevatorAnim.SetBool("isTopFloor", true);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => elevatorAnim.GetCurrentAnimatorStateInfo(elevatorAnim.GetLayerIndex("Position")).normalizedTime >= 1);
        elevatorAnim.SetBool("areDoorsOpen", true);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => elevatorAnim.GetCurrentAnimatorStateInfo(elevatorAnim.GetLayerIndex("Door")).normalizedTime >= 1);
        yield return new WaitForSeconds(3);
        elevatorAnim.SetBool("areDoorsOpen", false);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => elevatorAnim.GetCurrentAnimatorStateInfo(elevatorAnim.GetLayerIndex("Door")).normalizedTime >= 1);
        elevatorAnim.SetBool("isTopFloor", false);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => elevatorAnim.GetCurrentAnimatorStateInfo(elevatorAnim.GetLayerIndex("Position")).normalizedTime >= 1);
        elevatorAnim.SetBool("areDoorsOpen", true);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => elevatorAnim.GetCurrentAnimatorStateInfo(elevatorAnim.GetLayerIndex("Door")).normalizedTime >= 1);
        yield return new WaitForSeconds(1);
        isMoving = false;
    }

    //SPAWN NPC COROUTINE
    private IEnumerator SpawnTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTimer + elevatorQueue.Count);
            SpawnNPC();
        }
    }
    //UNITY FUNCTIONS
    void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnTimer());
    }

    void Update()
    {

    }
}
