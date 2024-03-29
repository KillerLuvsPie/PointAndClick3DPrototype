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
    private float rotationYSpeed = 85;
    private Animator npcAnimator;
    private AudioSource audioSource;

    public void MoveTo(Vector3 position)
    {
        Vector3 offsetPosition = new Vector3(ElevatorManager.Instance.npcWaitOffset * queuePosition, 0,0);
        nma.SetDestination(position + offsetPosition);
    }

    public void Rotate(Vector3 rotation)
    {
        transform.Rotate(rotation);
    }

    public void Delete()
    {
        ElevatorManager.Instance.elevatorQueue.RemoveAt(0);
        ElevatorManager.Instance.NPCQueueAdjustment();
        Destroy(gameObject);
    }

    //SOUND FUNCTIONS
    public void PlayFootstepSound()
    {
        SoundManager.Instance.PlayOneSound
        (
            SoundManager.Instance.sfx_footsteps[Random.Range(0,SoundManager.Instance.sfx_footsteps.Length)],
            audioSource,
            npcAnimator.GetFloat("Movement") * audioSource.volume
        );
    }

    //PROCESS FUNCTION
    private IEnumerator ProcessAction()
    {
        bool stop = false;
        bool rotate = true;
        while(!stop)
        {
            switch(state)
            {
                case DataVariables.ElevatorNPCState.Spawn:
                    queuePosition = ElevatorManager.Instance.elevatorQueue.Count;
                    ElevatorManager.Instance.elevatorQueue.Add(this);
                    MoveTo(ElevatorManager.Instance.npcWaitPosition.position);
                    state = DataVariables.ElevatorNPCState.Move;
                    break;
                case DataVariables.ElevatorNPCState.Move:
                    yield return new WaitForSeconds(0.25f);
                    yield return new WaitUntil(() => nma.remainingDistance <= 1);
                    yield return new WaitUntil(() => nma.velocity.magnitude <= 0);
                    nma.speed = 2;
                    while(rotate)
                    {
                        if(transform.rotation.eulerAngles.y > 90 && transform.rotation.eulerAngles.y < 270)
                        {
                            transform.Rotate(0, rotationYSpeed * Time.deltaTime, 0);
                        }
                        else
                        {
                            transform.Rotate(0, -rotationYSpeed * Time.deltaTime, 0);
                        }
                        if(Mathf.Abs(transform.rotation.eulerAngles.y - 270) <= 1)
                        {
                            rotate = false;
                        }
                        yield return new WaitForEndOfFrame();
                    }
                    state = DataVariables.ElevatorNPCState.Wait;
                    break;
                case DataVariables.ElevatorNPCState.Wait:
                    yield return new WaitForSeconds(1);
                    if(ElevatorManager.Instance.isMoving == false && queuePosition == 0)
                    {
                        MoveTo(ElevatorManager.Instance.elevatorPosition.position);
                        state = DataVariables.ElevatorNPCState.Enter;
                    }
                    break;
                case DataVariables.ElevatorNPCState.Enter:
                    yield return new WaitForSeconds(0.5f);
                    yield return new WaitUntil(() => nma.remainingDistance <= 0.5f);
                    yield return new WaitUntil(() => nma.velocity.magnitude <= 0);
                    ElevatorManager.Instance.StartElevatorCoroutine(this);
                    stop = !stop;
                    break;
                default:
                print("Could not find state for roaming npc " + gameObject.name);
                    break;
            }
        }
    }

    void Start()
    {
        npcAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if(isRoaming)
        {
            nma = GetComponent<NavMeshAgent>();
            nma.updateRotation = true;
            state = DataVariables.ElevatorNPCState.Spawn;
            StartCoroutine(ProcessAction());
        }
        else
        {
            state = DataVariables.ElevatorNPCState.Idle;
            npcAnimator.SetFloat("Movement", 0);
            SoundManager.Instance.allAudioSources.Add(audioSource);
        }
    }

    void Update()
    {
        if(isRoaming)
            npcAnimator.SetFloat("Movement",  Mathf.Clamp01( nma.velocity.magnitude/nma.speed));
    }
}
