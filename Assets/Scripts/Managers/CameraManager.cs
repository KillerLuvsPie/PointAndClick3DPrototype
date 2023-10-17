using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    public List<Transform> cameraAnchors = new List<Transform>();
    float offset = 15;
    void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    void Update()
    {
        if(PlayerController.Instance.transform.position.x >= 15)
        {
            Camera.main.transform.position = new Vector3(cameraAnchors[0].position.x, cameraAnchors[0].position.y, PlayerController.Instance.transform.position.z + offset);
            Camera.main.transform.rotation = cameraAnchors[0].rotation;
        }
            
        else if(PlayerController.Instance.transform.position.x <= -15)
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