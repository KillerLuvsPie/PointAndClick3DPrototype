using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractibleObject : MonoBehaviour
{
    //ACTIVE FLAG
    public bool isActive = true;
    //REFERENCES
    private GameObject clickIndicator;
    private Animator clickIndicatorAnim;
    private Image clickIndicatorImage;
    //PROPERTIES
    public float interactRadius = 2.5f;
    private float minDistance = 15;
    private float maxDistance = 30;
    private Color alpha;

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
        clickIndicatorImage = clickIndicator.GetComponent<Image>();
        alpha = clickIndicatorImage.color;
        if(isActive == false)
            clickIndicator.SetActive(false);
    }

    void Update()
    {
        clickIndicator.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position + Vector3.up * 1.5f);
        float distance = Vector3.Distance(PlayerController.Instance.transform.position, transform.position);
        float lerp = 1 - Mathf.Clamp01((distance - minDistance)/(maxDistance - minDistance));
        alpha.a = lerp;
        clickIndicatorImage.color = alpha;
    }
}
