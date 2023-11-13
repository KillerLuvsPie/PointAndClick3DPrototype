using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractibleObject : MonoBehaviour
{
    //ACTIVE FLAG
    public bool isActive = true;
    //REFERENCES
    private GameObject clickIndicator;
    private Animator clickIndicatorAnim;
    //PROPERTIES
    public float interactRadius = 2.5f;

    public void ActivateObject()
    {
        isActive = true;
        clickIndicator.SetActive(true);
    }
    //UNITY FUNCTIONS
    void OnMouseEnter()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
            clickIndicatorAnim.SetBool("mouseover", true);
        
    }
    void OnMouseExit()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
            clickIndicatorAnim.SetBool("mouseover", false);
    }
    void OnMouseDown()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
            PlayerController.Instance.QueueInteraction(gameObject, interactRadius);
    }
    
    void Start()
    {
        clickIndicator = Instantiate(UIManager.Instance.clickIndicatorPrefab, UIManager.Instance.clickIndicators);
        clickIndicatorAnim = clickIndicator.GetComponent<Animator>();
        if(isActive == false)
            clickIndicator.SetActive(false);
    }

    void Update()
    {
        clickIndicator.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position + Vector3.up * 1.5f);
    }
}
