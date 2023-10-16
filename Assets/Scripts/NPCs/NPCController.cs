using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCController : MonoBehaviour
{
    public string message;
    public GameObject interactIndicator;
    public float interactRadius = 2.5f;
    public Animator interactIndicatorAnim;
    
    void OnMouseEnter()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            //interactIndicator.transform.localScale = new Vector3(1,1,1);
            interactIndicatorAnim.SetBool("mouseover", true);
        }
    }
    void OnMouseExit()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            //interactIndicator.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
            interactIndicatorAnim.SetBool("mouseover", false);
        }
    }
    void OnMouseDown()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
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
