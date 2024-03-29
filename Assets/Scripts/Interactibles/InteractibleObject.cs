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
    private Transform clickIndicatorPosition;
    private Animator clickIndicatorAnim;
    private Image clickIndicatorImage;
    //PROPERTIES
    public float interactRadius = 2.5f;
    public bool ignoreYPos = false;
    private float minAlphaDistance = 15;
    private float maxAlphaDistance = 30;
    private Color alpha;
    //FUNCTIONS
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
            PlayerController.Instance.QueueInteraction(gameObject, interactRadius, ignoreYPos);
    }
    
    void Start()
    {
        clickIndicator = Instantiate(UIManager.Instance.clickIndicatorPrefab, UIManager.Instance.clickIndicators);
        clickIndicatorPosition = transform.GetChild(0);
        clickIndicatorAnim = clickIndicator.GetComponent<Animator>();
        clickIndicatorImage = clickIndicator.GetComponent<Image>();
        alpha = clickIndicatorImage.color;
        if(isActive == false)
            clickIndicator.SetActive(false);
    }

    void Update()
    {
        if(clickIndicator.activeSelf == true)
        {
            clickIndicator.transform.position = Camera.main.WorldToScreenPoint(clickIndicatorPosition.position);
            float distance = Vector3.Distance(PlayerController.Instance.transform.position, transform.position);
            float lerp = 1 - Mathf.Clamp01((distance - minAlphaDistance)/(maxAlphaDistance - minAlphaDistance));
            alpha.a = lerp;
            clickIndicatorImage.color = alpha;
        }   
    }
}
