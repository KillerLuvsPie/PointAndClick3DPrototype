using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraCollider : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            vcam.Priority = 1;
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
