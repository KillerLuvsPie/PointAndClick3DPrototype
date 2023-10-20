using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    public List<Transform> cameraAnchors = new List<Transform>();
    float leftTrigger = 15;
    float rightTrigger = -15;
    float offset = 15;

    
    //UNITY FUNCTIONS
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("CameraCollider"))
        {
            CameraCollider camCol = col.GetComponent<CameraCollider>();
            
        }
    }
    
    void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    void Update()
    {
        if(PlayerController.Instance.transform.position.x >= leftTrigger)
        {
            Camera.main.transform.position = new Vector3(cameraAnchors[0].position.x, cameraAnchors[0].position.y, PlayerController.Instance.transform.position.z + offset);
            Camera.main.transform.rotation = cameraAnchors[0].rotation;
        }
            
        else if(PlayerController.Instance.transform.position.x <= rightTrigger)
        {
            Camera.main.transform.position = new Vector3(cameraAnchors[4].position.x, cameraAnchors[4].position.y, PlayerController.Instance.transform.position.z + offset);
            Camera.main.transform.rotation = cameraAnchors[4].rotation;
        }
            
        else
        {
            Camera.main.transform.position = new Vector3(cameraAnchors[2].position.x, cameraAnchors[2].position.y, PlayerController.Instance.transform.position.z + offset);
            Camera.main.transform.rotation = cameraAnchors[2].rotation;
        }
    }
}