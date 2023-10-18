using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractibleObject : MonoBehaviour
{
    public float interactRadius = 2.5f;
    public Animator interactIndicatorAnim;
    
    void OnMouseEnter()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
            interactIndicatorAnim.SetBool("mouseover", true);
        
    }
    void OnMouseExit()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
            interactIndicatorAnim.SetBool("mouseover", false);
    }
    void OnMouseDown()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
            PlayerController.Instance.QueueInteraction(gameObject, interactRadius);
    }
}
