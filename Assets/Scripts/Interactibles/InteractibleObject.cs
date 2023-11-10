using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractibleObject : MonoBehaviour
{
    public float interactRadius = 2.5f;
    private GameObject clickIndicator;
    private Animator interactIndicatorAnim;
    
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

    void Start()
    {
        clickIndicator = Instantiate(UIManager.Instance.clickIndicatorPrefab, UIManager.Instance.clickIndicators);
        interactIndicatorAnim = clickIndicator.GetComponent<Animator>();
    }

    void Update()
    {
        clickIndicator.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position + Vector3.up * 1.5f);
    }
}
