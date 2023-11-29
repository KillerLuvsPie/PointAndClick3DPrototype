using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    //VARIABLES
    public Transform cmCam;
    private Collider rotateCol;
    private float startZPos;
    private float rotationRange = 10;

    //UNITY FUNCTIONS
    void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            float lerp = Mathf.Clamp01((col.transform.position.z - rotateCol.bounds.min.z)/(rotateCol.bounds.max.z - rotateCol.bounds.min.z));
            cmCam.eulerAngles = new Vector3(cmCam.rotation.eulerAngles.x, startZPos + Mathf.Lerp(0, rotationRange, lerp), cmCam.rotation.eulerAngles.z);
        }
    }

    void Awake()
    {
        rotateCol = GetComponent<Collider>();
        cmCam.eulerAngles = new Vector3(cmCam.rotation.eulerAngles.x, cmCam.rotation.eulerAngles.y - (rotationRange/2) ,cmCam.rotation.eulerAngles.z);
        startZPos = cmCam.rotation.eulerAngles.y;
    }
}
