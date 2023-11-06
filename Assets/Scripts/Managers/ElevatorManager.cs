using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    //SINGLETON
    public static ElevatorManager Instance;
    //ELEVATOR ANIMATOR
    public Animator elevatorAnim;
    //NPC PREFAB
    public GameObject roamingNPC;
    //NPC WRAPPER
    public Transform actorWrapper;
    //POSITiONS
    public Transform npcWaitPosition;
    public float npcWaitOffset =  1.5f;
    public List<Transform> spawnPoints;
    //NPC LIST
    public List<GameObject> elevatorQueue = new List<GameObject>();
    //SPAWN TIMER
    private int waitTimer = 4;

    //SPAWN FUNCTION
    private void SpawnNPC()
    {
        GameObject npc = Instantiate(roamingNPC, spawnPoints[Random.Range(0,spawnPoints.Count)].position , Quaternion.identity);
        npc.transform.parent = actorWrapper;
    }

    //COROUTINE
    private IEnumerator SpawnTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTimer);
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
