using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public string message;
    public float interactRadius = 2.5f;
    void OnMouseDown()
    {
        PlayerController.Instance.QueueInteraction(gameObject, message, interactRadius);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
