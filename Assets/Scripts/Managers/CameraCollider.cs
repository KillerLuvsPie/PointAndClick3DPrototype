using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraCollider : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public int camPriority = 1;

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            vcam.Priority = camPriority;
        }
            
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            vcam.Priority = 0;
        }
    }
}
